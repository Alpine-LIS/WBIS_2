//using Alpine.Announcements;
using Atlas3.Manager;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace WBIS_2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public MainViewModel()
        {
            //ShowAnnouncementsWindowCommand =
            //    new DelegateCommand(ShowAnnouncementsWindow_Execute, ShowAnnouncementsWindow_CanExecute);
            //Announcements.OnConfiguringAnnouncements += OnConfiguringAnnouncements;
            //Announcements.Instance.PropertyChanged += AnnouncementsInstanceOnPropertyChanged;
            //AtlasManager.Instance.OnReadyToWork += AtlasManagerInstanceOnOnReadyToWork;
            //RaisePropertiesChanged(nameof(UnViewedAnnouncementsVisibility), nameof(UnViewedAnnouncementsCount));
        }

        //private Announcements.AnnouncementsConfig OnConfiguringAnnouncements()
        //{
        //    var config = new Announcements.AnnouncementsConfig();
        //    //var useLocalFile = false;
        //    //if (useLocalFile)
        //    //{
        //    //    config.Source = @"D:\Work.Atlas\Data\Announcements\Announcements.md";
        //    //}
        //    //else
        //    {
        //        config.Source = @"s3://alpine-announcments/WBIS 2/Announcements.md";
        //        config.AwsAccessKey = ConfigurationManager.AppSettings["AWS_KEY"];
        //        config.AwsSecretKey = ConfigurationManager.AppSettings["AWS_SKEY"];
        //    }

        //    return config;
        //}

        //public Visibility UnViewedAnnouncementsVisibility =>
        //    Announcements.Instance.UnViewedCount > 0 ? Visibility.Visible : Visibility.Collapsed;

        //public int UnViewedAnnouncementsCount => 3;// Announcements.Instance.UnViewedCount;

        //public DelegateCommand ShowAnnouncementsWindowCommand { get; protected set; }

        //private bool ShowAnnouncementsWindow_CanExecute()
        //{
        //    return Announcements.Instance.Records.Length > 0;
        //}

        //private void ShowAnnouncementsWindow_Execute()
        //{
        //    Announcements.Instance.ShowWindow(true);
        //}

        //private void AnnouncementsInstanceOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(Announcements.Instance.Records))
        //    {
        //        ShowAnnouncementsWindowCommand.RaiseCanExecuteChanged();
        //    }

        //    if (e.PropertyName == nameof(Announcements.Instance.UnViewedCount))
        //    {
        //        RaisePropertiesChanged(nameof(UnViewedAnnouncementsVisibility), nameof(UnViewedAnnouncementsCount));
        //    }
        //}

        //private void AtlasManagerInstanceOnOnReadyToWork(object? sender, EventArgs e)
        //{
        //    Announcements.Instance.ShowWindow(false);
        //}

        //public void ShowAnnouncmentsStartup()
        //{
        //    if (Announcements.Instance.UnViewedCount > 0 && Announcements.Instance.ShowOnAppStart)
        //        Announcements.Instance.ShowWindow(true);
        //}
    }
}
