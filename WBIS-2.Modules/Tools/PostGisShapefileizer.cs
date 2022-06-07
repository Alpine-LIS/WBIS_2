using Atlas.Data;
using Atlas.Projections;
using DevExpress.Mvvm;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WBIS_2.DataModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace WBIS_2.Modules.Tools
{
    public class PostGisShapefileConverter
    {
        public PostGisShapefileConverter(Type i, IQueryable records, string fileStr)
        {
            var geoProp = i.GetProperty("Geometry");
            if (geoProp == null) return;
            Shapefile ShapeFile = NewShapefile(fileStr, geoProp);
            ShapeFile.Projection = ProjectionInfo.FromEpsgCode(26710);

            List<PropertyColumn> PropertyColumns = new List<PropertyColumn>();
            PropertyColumnBuilder(ref PropertyColumns, i);

            foreach (var col in PropertyColumns)
            {
                ShapeFile.DataTable.Columns.Add(col.ShapefileColumnStr, col.DataType);
            }
            ShapeFile.SaveAs(fileStr, true);

            foreach (var record in records)
            {
                var feat = ShapeFile.AddFeature((Geometry)geoProp.GetValue(record));
                foreach (var col in PropertyColumns)
                {
                    if (col.PropertyName.Contains("."))
                    {
                        PropertyInfo prop = null;
                        object val = null;
                        var parts = col.PropertyName.Split('.');
                        foreach (var part in parts)
                        {
                            if (prop == null)
                            {
                                prop = i.GetProperty(part);
                                val = prop.GetValue(record);
                            }
                            else
                            {
                                prop = prop.PropertyType.GetProperty(part);
                                if (val != null)
                                    val = prop.GetValue(val);
                            }
                        }
                        if (val != null)
                            feat.DataRow[col.ShapefileColumnStr] = val;
                    }
                    else
                    {
                        var prop = i.GetProperty(col.PropertyName);
                        var val = prop.GetValue(record);
                        if (val != null)
                            feat.DataRow[col.ShapefileColumnStr] = val;
                    }
                }
            }
            ShapeFile.InitializeVertices();
            ShapeFile.SaveAs(fileStr, true);
        }
        private Shapefile NewShapefile(string fileStr, PropertyInfo geoProp)
        {
            if (geoProp == null) return null;

            if (geoProp.PropertyType == typeof(NetTopologySuite.Geometries.Point))
                return new PointShapefile(fileStr);
            else if (geoProp.PropertyType == typeof(NetTopologySuite.Geometries.LineString))
                return new LineShapefile(fileStr);
            else if (geoProp.PropertyType == typeof(NetTopologySuite.Geometries.MultiLineString))
                return new LineShapefile(fileStr);
            else if (geoProp.PropertyType == typeof(NetTopologySuite.Geometries.Polygon))
                return new PolygonShapefile(fileStr);
            else if (geoProp.PropertyType == typeof(NetTopologySuite.Geometries.MultiPolygon))
                return new PolygonShapefile(fileStr);
            else return null;
        }

        public void PropertyColumnBuilder(ref List<PropertyColumn> PropertyColumns, Type i)
        {
            var properties = i.GetProperties();
            foreach (var prop in properties)
            {
                if (!WiteableProperty(prop)) continue;
                else if (prop.PropertyType.Namespace.Contains("WBIS_2.DataModel"))
                {
                    var listInfo = prop.GetCustomAttribute(typeof(ListInfo));
                    if (listInfo != null)
                    {
                        if (((ListInfo)listInfo).AutoInclude)
                        {
                            IncludedPropertyColumnBuilder(ref PropertyColumns, prop.PropertyType, $"{prop.Name}");
                        }
                    }
                }
                else
                {
                    AddtoPropertyColumns(ref PropertyColumns, $"{prop.Name}", prop.PropertyType);
                }
            }
        }
        public void IncludedPropertyColumnBuilder(ref List<PropertyColumn> PropertyColumns, Type i, string fieldText)
        {
            var properties = i.GetProperties();
            foreach (var prop in properties)
            {
                if (!WiteableProperty(prop)) continue;
                var listInfo = prop.GetCustomAttribute(typeof(ListInfo));
                if (listInfo != null)
                {
                    //if (((ListInfo)listInfo).AutoInclude)
                    //{
                    //    IncludedPropertyColumnBuilder(ref PropertyColumns, prop.PropertyType, $"{fieldText}.{prop.Name}");
                    //}
                    if (((ListInfo)listInfo).DisplayField)
                    {
                        if (prop.PropertyType.Namespace.Contains("WBIS_2.DataModel"))
                            IncludedPropertyColumnBuilder(ref PropertyColumns, prop.PropertyType, $"{fieldText}.{prop.Name}");
                        else
                            AddtoPropertyColumns(ref PropertyColumns, $"{fieldText}.{prop.Name}", prop.PropertyType);
                    }
                }
            }
        }
        private bool WiteableProperty(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(Guid) && prop.Name != "Guid") return false;
            else if (prop.Name.Contains("Geometry")) return false;
            else if (prop.PropertyType.Name.Contains("Collection")) return false;
            else if (prop.DeclaringType == typeof(UserDataValidator)) return false;
            else if (prop.PropertyType.IsGenericType)
                if (prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    if (Nullable.GetUnderlyingType(prop.PropertyType) == typeof(Guid)) return false;

            return true;
        }

        private void AddtoPropertyColumns(ref List<PropertyColumn> PropertyColumns, string propName, Type propType)
        {
            var p = new PropertyColumn()
            {
                PropertyName = propName,
                DataType = propType
            };

            string test = p.PropertyName.Split('.').Last();
            test = MinimizeString(test);
            if (PropertyColumns.Any(_ => _.ShapefileColumnStr == test))
            {
                test = p.PropertyName.Split('.').First();
                test = MinimizeString(test);
            }
            p.ShapefileColumnStr = test;

            PropertyColumns.Add(p);
        }
        private string MinimizeString(string val)
        {
            string[] vouls = new string[] { "a", "e", "i", "o", "u", " " };

            foreach (string voul in vouls)
            {
                if (val.Length <= 10) break;
                val = val.Replace(voul, "");
            }

            if (val.Length > 10)
            {
                val = val.Replace("_", "");
            }
            if (val.Length > 10)
            {
                val = val.Substring(0, 10);
            }
            return val;
        }

        public class PropertyColumn : BindableBase
        {
            public string PropertyName
            {
                get { return GetProperty(() => PropertyName); }
                set
                {
                    SetProperty(() => PropertyName, value);
                    ColumnName = PropertyName.Replace(".", "_");
                }
            }
            public string ColumnName
            {
                get { return GetProperty(() => ColumnName); }
                set
                {
                    SetProperty(() => ColumnName, value);
                }
            }
            public string ShapefileColumnStr { get; set; }
            public Type DataType
            {
                get { return GetProperty(() => DataType); }
                set
                {
                    SetProperty(() => DataType, value);

                    if (DataType.IsGenericType)
                        if (DataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            DataType = Nullable.GetUnderlyingType(DataType);
                }
            }
        }
    }
}
