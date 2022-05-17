using Microsoft.Win32;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;
using Atlas.Data;
using System.Windows;
using NetTopologySuite.Geometries.Implementation;
using System.Reflection;

namespace WBIS_2.Modules.Tools
{
    public  class RecordFeatureBuilder
    {      
        public Geometry ExternalFeature(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(MultiPolygon))
                return ExternalFeatureMPoly();
            else if (propertyInfo.PropertyType == typeof(LineString))
                return ExternalFeatureLine();
            else
                return ExternalFeaturePoint();
        }

        public MultiPolygon ExternalFeatureMPoly()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            if (!ofd.ShowDialog().Value) return null;

            var shp = Shapefile.OpenFile(ofd.FileName);
            if (shp.FeatureType != FeatureType.Polygon)
            {
                MessageBox.Show("The selected shapefile must contain polygons.");
                return null;
            }

            PolygonShapefile polygonShapefile = new PolygonShapefile(ofd.FileName);

            if (polygonShapefile.Features.Count > 1)
            {
                if (MessageBox.Show("Each record can only have one feature. The features in the selected shapefile will be unionized.", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return null;

                var geo = polygonShapefile.Features[0].Geometry;
                for (int i = 1; i < polygonShapefile.Features.Count; i++)
                {
                    geo = geo.Union(polygonShapefile.Features[i].Geometry);
                }

                CoordinateArraySequenceFactory factory = CoordinateArraySequenceFactory.Instance;

                MultiPolygon returnVal;
                //To prevent z axis issue
                if (geo is NetTopologySuite.Geometries.Polygon)
                {
                    CoordinateSequence sequence = CopyToSequence(geo.Coordinates, factory.Create(geo.Coordinates.Length, 2));
                    Polygon polygon = GeometryFactory.Default.CreatePolygon(sequence);
                    returnVal = new MultiPolygon(new Polygon[] { polygon });
                }
                else
                {
                    List<Polygon> polygons = new List<Polygon>();
                    foreach (Geometry geometry in ((MultiPolygon)geo).Geometries)
                    {
                        CoordinateSequence sequence = CopyToSequence(geometry.Coordinates, factory.Create(geometry.Coordinates.Length, 2));
                        polygons.Add(GeometryFactory.Default.CreatePolygon(sequence));
                    }
                    returnVal = new MultiPolygon(polygons.ToArray());
                }

                if (returnVal.SRID == 0) returnVal.SRID = 26710;
                return returnVal;
            }
            else
            {
                Geometry geo = polygonShapefile.Features[0].Geometry;
                if (geo is NetTopologySuite.Geometries.Polygon) geo = new MultiPolygon(new Polygon[] { (Polygon)geo });
                if (geo.SRID == 0) geo.SRID = 26710;

                return (MultiPolygon)geo;
            }
        }
            
        public LineString ExternalFeatureLine()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            if (!ofd.ShowDialog().Value) return null;

            var shp = Shapefile.OpenFile(ofd.FileName);
            if (shp.FeatureType != FeatureType.Line)
            {
                MessageBox.Show("The selected shapefile must contain lines.");
                return null;
            }

            LineShapefile lineShapefile = new LineShapefile(ofd.FileName);

            if (lineShapefile.Features.Count > 1)
            {
                if (MessageBox.Show("Each record can only have one feature. The features in the selected shapefile will be unionized.", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return null;

                var geo = lineShapefile.Features[0].Geometry;
                for (int i = 1; i < lineShapefile.Features.Count; i++)
                {
                    geo = geo.Union(lineShapefile.Features[i].Geometry);
                }

                CoordinateArraySequenceFactory factory = CoordinateArraySequenceFactory.Instance;

                LineString returnVal = null;
                //To prevent z axis issue
                if (geo is NetTopologySuite.Geometries.LineString)
                {
                    CoordinateSequence sequence = CopyToSequence(geo.Coordinates, factory.Create(geo.Coordinates.Length, 2));
                    returnVal = GeometryFactory.Default.CreateLineString  (sequence);
                    if (returnVal.SRID == 0) returnVal.SRID = 26710;
                }
                return returnVal;
            }
            else
            {
                LineString returnVal = (LineString)lineShapefile.Features[0].Geometry;
                if (returnVal.SRID == 0) returnVal.SRID = 26710;

                return (LineString)returnVal;
            }
        }

        public NetTopologySuite.Geometries.Point ExternalFeaturePoint()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SHP|*.shp";
            if (!ofd.ShowDialog().Value) return null;

            var shp = Shapefile.OpenFile(ofd.FileName);
            if (shp.FeatureType != FeatureType.Point)
            {
                MessageBox.Show("The selected shapefile must contain points.");
                return null;
            }

            PointShapefile pointShapefile = new PointShapefile(ofd.FileName);

            if (pointShapefile.Features.Count > 1)
            {
                if (MessageBox.Show("Each record can only have one feature. The first feature will be used.", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return null;
            }
            NetTopologySuite.Geometries.Point returnVal = (NetTopologySuite.Geometries.Point)pointShapefile.Features[0].Geometry;
                if (returnVal.SRID == 0) returnVal.SRID = 26710;

                return (NetTopologySuite.Geometries.Point)returnVal;
        }


        private CoordinateSequence CopyToSequence(Coordinate[] coords, CoordinateSequence sequence)
        {
            for (int i = 0; i < coords.Length; i++)
            {
                sequence.SetOrdinate(i, Ordinate.X, coords[i].X);
                sequence.SetOrdinate(i, Ordinate.Y, coords[i].Y);
            }
            return sequence;
        }
    }   
}
