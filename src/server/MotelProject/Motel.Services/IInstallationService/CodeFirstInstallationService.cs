using Motel.Core.Caching;
using Motel.Domain.Domain.Lester;
using Motel.Domain.Domain.Media;
using Motel.Services.Helper;
using Motel.Services.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.IInstallationService
{
    public partial class CodeFirstInstallationService
    {
        //protected virtual void InstallSettings()
        //{
        //    var settingService = EngineContext.Current.Resolve<ISettingService>();
        //    settingService.SaveSetting(new PdfSettings
        //    {
        //        LogoPictureId = 0,
        //        LetterPageSizeEnabled = false,
        //        RenderOrderNotes = true,
        //        FontFileName = "FreeSerif.ttf",
        //        InvoiceFooterTextColumn1 = null,
        //        InvoiceFooterTextColumn2 = null
        //    });

        //    settingService.SaveSetting(new SitemapSettings
        //    {
        //        SitemapEnabled = true,
        //        SitemapPageSize = 200,
        //        SitemapIncludeCategories = true,
        //        SitemapIncludeManufacturers = true,
        //        SitemapIncludeProducts = false,
        //        SitemapIncludeProductTags = false,
        //        SitemapIncludeBlogPosts = true,
        //        SitemapIncludeNews = false,
        //        SitemapIncludeTopics = true
        //    });

        //    settingService.SaveSetting(new SitemapXmlSettings
        //    {
        //        SitemapXmlEnabled = true,
        //        SitemapXmlIncludeBlogPosts = true,
        //        SitemapXmlIncludeCategories = true,
        //        SitemapXmlIncludeManufacturers = true,
        //        SitemapXmlIncludeNews = true,
        //        SitemapXmlIncludeProducts = true,
        //        SitemapXmlIncludeProductTags = true,
        //        SitemapXmlIncludeCustomUrls = true,
        //        SitemapXmlIncludeTopics = true
        //    });

        //    settingService.SaveSetting(new CommonSettings
        //    {
        //        UseSystemEmailForContactUsForm = true,

        //        DisplayJavaScriptDisabledWarning = false,
        //        UseFullTextSearch = false,
        //        FullTextMode = FulltextSearchMode.ExactMatch,
        //        Log404Errors = true,
        //        BreadcrumbDelimiter = "/",
        //        BbcodeEditorOpenLinksInNewWindow = false,
        //        PopupForTermsOfServiceLinks = true,
        //        JqueryMigrateScriptLoggingActive = false,
        //        SupportPreviousNopcommerceVersions = true,
        //        UseResponseCompression = true,
        //        StaticFilesCacheControl = "public,max-age=31536000",
        //        FaviconAndAppIconsHeadCode = "<link rel=\"apple-touch-icon\" sizes=\"180x180\" href=\"/icons/icons_0/apple-touch-icon.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"32x32\" href=\"/icons/icons_0/favicon-32x32.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"192x192\" href=\"/icons/icons_0/android-chrome-192x192.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"16x16\" href=\"/icons/icons_0/favicon-16x16.png\"><link rel=\"manifest\" href=\"/icons/icons_0/site.webmanifest\"><link rel=\"mask-icon\" href=\"/icons/icons_0/safari-pinned-tab.svg\" color=\"#5bbad5\"><link rel=\"shortcut icon\" href=\"/icons/icons_0/favicon.ico\"><meta name=\"msapplication-TileColor\" content=\"#2d89ef\"><meta name=\"msapplication-TileImage\" content=\"/icons/icons_0/mstile-144x144.png\"><meta name=\"msapplication-config\" content=\"/icons/icons_0/browserconfig.xml\"><meta name=\"theme-color\" content=\"#ffffff\">",
        //        EnableHtmlMinification = true,
        //        //we disable bundling out of the box because it requires a lot of server resources
        //        EnableJsBundling = false,
        //        EnableCssBundling = false,
        //        RestartTimeout = NopCommonDefaults.RestartTimeout
        //    });

        //    settingService.SaveSetting(new SeoSettings
        //    {
        //        PageTitleSeparator = ". ",
        //        PageTitleSeoAdjustment = PageTitleSeoAdjustment.PagenameAfterStorename,
        //        DefaultTitle = "Your store",
        //        DefaultMetaKeywords = string.Empty,
        //        DefaultMetaDescription = string.Empty,
        //        GenerateProductMetaDescription = true,
        //        ConvertNonWesternChars = false,
        //        AllowUnicodeCharsInUrls = true,
        //        CanonicalUrlsEnabled = false,
        //        QueryStringInCanonicalUrlsEnabled = false,
        //        WwwRequirement = WwwRequirement.NoMatter,
        //        TwitterMetaTags = true,
        //        OpenGraphMetaTags = true,
        //        MicrodataEnabled = true,
        //        ReservedUrlRecordSlugs = new List<string>
        //        {
        //            "admin",
        //            "install",
        //            "recentlyviewedproducts",
        //            "newproducts",
        //            "compareproducts",
        //            "clearcomparelist",
        //            "setproductreviewhelpfulness",
        //            "login",
        //            "register",
        //            "logout",
        //            "cart",
        //            "wishlist",
        //            "emailwishlist",
        //            "checkout",
        //            "onepagecheckout",
        //            "contactus",
        //            "passwordrecovery",
        //            "subscribenewsletter",
        //            "blog",
        //            "boards",
        //            "inboxupdate",
        //            "sentupdate",
        //            "news",
        //            "sitemap",
        //            "search",
        //            "config",
        //            "eucookielawaccept",
        //            "page-not-found",
        //            "products",
        //            //system names are not allowed (anyway they will cause a runtime error),
        //            "con",
        //            "lpt1",
        //            "lpt2",
        //            "lpt3",
        //            "lpt4",
        //            "lpt5",
        //            "lpt6",
        //            "lpt7",
        //            "lpt8",
        //            "lpt9",
        //            "com1",
        //            "com2",
        //            "com3",
        //            "com4",
        //            "com5",
        //            "com6",
        //            "com7",
        //            "com8",
        //            "com9",
        //            "null",
        //            "prn",
        //            "aux"
        //        },
        //        CustomHeadTags = string.Empty
        //    });

        //    settingService.SaveSetting(new AdminAreaSettings
        //    {
        //        DefaultGridPageSize = 15,
        //        PopupGridPageSize = 7,
        //        GridPageSizes = "7, 15, 20, 50, 100",
        //        RichEditorAdditionalSettings = null,
        //        RichEditorAllowJavaScript = false,
        //        RichEditorAllowStyleTag = false,
        //        UseRichEditorForCustomerEmails = false,
        //        UseRichEditorInMessageTemplates = false,
        //        CheckCopyrightRemovalKey = true,
        //        UseIsoDateFormatInJsonResult = true
        //    });

         
         
        //    settingService.SaveSetting(new LesterSettings
        //    {
        //        UsernamesEnabled = false,
        //        CheckUsernameAvailabilityEnabled = false,
        //        AllowUsersToChangeUsernames = false,
        //        HashedPasswordFormat = MotelUserServicesDefaults.DefaultHashedPasswordFormat,
        //        PasswordMinLength = 6,
        //        PasswordRequireDigit = false,
        //        PasswordRequireLowercase = false,
        //        PasswordRequireNonAlphanumeric = false,
        //        PasswordRequireUppercase = false,
        //        UnduplicatedPasswordsNumber = 4,
        //        PasswordRecoveryLinkDaysValid = 7,
        //        PasswordLifetime = 90,
        //        FailedPasswordAllowedAttempts = 0,
        //        FailedPasswordLockoutMinutes = 30,
        //        UserRegistrationType = UserRegistrationType.Standard,
        //        AllowCustomersToUploadAvatars = false,
        //        AvatarMaximumSizeBytes = 20000,
        //        DefaultAvatarEnabled = true,
        //        ShowCustomersLocation = false,
        //        ShowCustomersJoinDate = false,
        //        AllowViewingProfiles = false,
        //        NotifyNewCustomerRegistration = false,
        //        HideDownloadableProductsTab = false,
        //        HideBackInStockSubscriptionsTab = false,
        //        DownloadableProductsValidateUser = false,
        //        CustomerNameFormat = CustomerNameFormat.ShowFirstName,
        //        FirstNameEnabled = true,
        //        FirstNameRequired = true,
        //        LastNameEnabled = true,
        //        LastNameRequired = true,
        //        GenderEnabled = true,
        //        DateOfBirthEnabled = true,
        //        DateOfBirthRequired = false,
        //        DateOfBirthMinimumAge = null,
        //        CompanyEnabled = true,
        //        StreetAddressEnabled = false,
        //        StreetAddress2Enabled = false,
        //        ZipPostalCodeEnabled = false,
        //        CityEnabled = false,
        //        CountyEnabled = false,
        //        CountyRequired = false,
        //        CountryEnabled = false,
        //        CountryRequired = false,
        //        StateProvinceEnabled = false,
        //        StateProvinceRequired = false,
        //        PhoneEnabled = false,
        //        FaxEnabled = false,
        //        AcceptPrivacyPolicyEnabled = false,
        //        NewsletterEnabled = true,
        //        NewsletterTickedByDefault = true,
        //        HideNewsletterBlock = false,
        //        NewsletterBlockAllowToUnsubscribe = false,
        //        OnlineCustomerMinutes = 20,
        //        StoreLastVisitedPage = false,
        //        StoreIpAddresses = true,
        //        LastActivityMinutes = 15,
        //        SuffixDeletedCustomers = false,
        //        EnteringEmailTwice = false,
        //        RequireRegistrationForDownloadableProducts = false,
        //        AllowCustomersToCheckGiftCardBalance = false,
        //        DeleteGuestTaskOlderThanMinutes = 1440,
        //        PhoneNumberValidationEnabled = false,
        //        PhoneNumberValidationUseRegex = false,
        //        PhoneNumberValidationRule = "^[0-9]{1,14}?$"
        //    });

           
           

        //    settingService.SaveSetting(new MediaSettings
        //    {
        //        AvatarPictureSize = 120,
        //        ProductThumbPictureSize = 415,
        //        ProductDetailsPictureSize = 550,
        //        ProductThumbPictureSizeOnProductDetailsPage = 100,
        //        AssociatedProductPictureSize = 220,
        //        CategoryThumbPictureSize = 450,
        //        ManufacturerThumbPictureSize = 420,
        //        VendorThumbPictureSize = 450,
        //        CartThumbPictureSize = 80,
        //        MiniCartThumbPictureSize = 70,
        //        AutoCompleteSearchThumbPictureSize = 20,
        //        ImageSquarePictureSize = 32,
        //        MaximumImageSize = 1980,
        //        DefaultPictureZoomEnabled = false,
        //        DefaultImageQuality = 80,
        //        MultipleThumbDirectories = false,
        //        ImportProductImagesUsingHash = true,
        //        AzureCacheControlHeader = string.Empty,
        //        UseAbsoluteImagePath = true
        //    });

       


        //    settingService.SaveSetting(new DateTimeSettings
        //    {
        //        DefaultStoreTimeZoneId = string.Empty,
        //        AllowCustomersToSetTimeZone = false
        //    });

        //    settingService.SaveSetting(new BlogSettings
        //    {
        //        Enabled = true,
        //        PostsPageSize = 10,
        //        AllowNotRegisteredUsersToLeaveComments = true,
        //        NotifyAboutNewBlogComments = false,
        //        NumberOfTags = 15,
        //        ShowHeaderRssUrl = false,
        //        BlogCommentsMustBeApproved = false,
        //        ShowBlogCommentsPerStore = false
        //    });
         
        //    settingService.SaveSetting(new CachingSettings
        //    {
        //        ShortTermCacheTime = 5,
        //        DefaultCacheTime = MotelCachingDefaults.CacheTime,
        //        BundledFilesCacheTime = 120
        //    });
        //}
    }
}
