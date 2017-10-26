using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Input;
using TonpeiFes.MobileCore.Services;
using Prism.Navigation;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Configurations;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class EnquetePageViewModel : ViewModelBase
    {
        private const string SECRET = "SECRET";
        private const string SECRET_STR = "回答しない";

        private static Dictionary<string, string> AGE = new Dictionary<string, string>
        {
            {"10歳未満", "AGE_00S"},
            {"10代", "AGE_10S"},
            {"20代", "AGE_20S"},
            {"30代", "AGE_30S"},
            {"40代", "AGE_40S"},
            {"50代", "AGE_50S"},
            {"60代以上", "AGE_60S"},
            {SECRET_STR, SECRET}
        };

        private static Dictionary<string, string> MEMBER = new Dictionary<string, string>
        {
            {"小学生", "PRIMARY_SCHOOL"},
            {"中学生", "JUNIORHIGH_SCHOOL"},
            {"高校生", "HIGH_SCHOOL"},
            {"東北大学生（１，２年生）", "TOHOKU_UNIV_12"},
            {"東北大学生（３，４年生）", "TOHOKU_UNIV_34"},
            {"東北大学院生", "TOHOKU_UNIV_GRAD"},
            {"他大学生", "OTHER_UNIV"},
            {"東北大学生の保護者", "TOHOKU_UNIV_PARENTS"},
            {"東北大学職員", "TOHOKU_UNIV_STAFF"},
            {"児童保護者", "PARENTS"},
            {"その他", "OTHER"},
            {SECRET_STR, SECRET}
        };

        private static Dictionary<string, string> RESIDENCE = new Dictionary<string, string>
        {
            {"青葉区", "SENDAI_AOBA"},
            {"泉区", "SENDAI_IZUMI"},
            {"太白区", "SENDAI_TAIHAKU"},
            {"宮城野区", "SENDAI_MIYAGINO"},
            {"若林区", "SENDAI_WAKABAYASHI"},
            {"仙台市外", "NOT_SENDAI"},
            {SECRET_STR, SECRET}
        };

        private static Dictionary<string, string> WHERE = new Dictionary<string, string>
        {
            {"ポスター", "POSTER"},
            {"SNS", "SOCIAL_MEDIA"},
            {"Webサイト", "WEB"},
            {"テレビ", "TV"},
            {"新聞記事", "NEWSPAPER"},
            {"口コミ", "WORD_OF_MOUSE"},
            {"日経新聞折込広告", "NIKKEI"},
            {"YouTube動画広告", "YouTube"},
            {"保育園/所のお便り等", "NURSERY"},
            {"その他", "OTHER"},
            {SECRET_STR, SECRET}
        };

        private static Dictionary<string, string> ACCESS = new Dictionary<string, string>
        {
            {"地下鉄", "SUBWAY"},
            {"バス", "BUS"},
            {"自転車", "BICYCLE"},
            {"バイク・原付", "BIKE"},
            {"徒歩", "FOOT"},
            {"その他", "OTHER"},
            {SECRET_STR, SECRET}
        };

        public List<string> Ages { get; }
        public List<string> Members { get; }
        public List<string> Residences { get; }
        public List<string> Wheres { get; }
        public List<string> Accesses { get; }

        public ReactiveProperty<string> AgeSelected { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> MemberSelected { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> ResidenceSelected { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> WhereSelected { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> AccessSelected { get; } = new ReactiveProperty<string>();

        private ReactiveProperty<string> AgeValidation;
        private ReactiveProperty<string> MemberValidation;
        private ReactiveProperty<string> ResidenceValidation;
        private ReactiveProperty<string> WhereValidation;
        private ReactiveProperty<string> AccessValidation;

        public ReactiveCommand SubmitCommand { get; }
        public ICommand SkipCommand { get; }
        public ICommand TermsOfUseCommand { get; }

        private INavigationService _navigationService;
        private IOpenWebPageService _webService;
        private IAnalyticsService _analyticsService;
        private ILocalConfigService _configService;

        public EnquetePageViewModel(INavigationService navigationService,
                                    IOpenWebPageService webService,
                                    IAnalyticsService analyticsService,
                                    ILocalConfigService configService,
                                    IConstUrls constUrls)
        {
            _navigationService = navigationService;
            _webService = webService;
            _analyticsService = analyticsService;
            _configService = configService;

            Ages = AGE.Keys.ToList();
            Members = MEMBER.Keys.ToList();
            Residences = RESIDENCE.Keys.ToList();
            Wheres = WHERE.Keys.ToList();
            Accesses = ACCESS.Keys.ToList();

            AgeValidation = AgeSelected.SetValidateNotifyError((val) => string.IsNullOrEmpty(val) ? "年齢が選択されていません" : null).AddTo(this.Disposable);
            MemberValidation = MemberSelected.SetValidateNotifyError((val) => string.IsNullOrEmpty(val) ? "所属が選択されていません" : null).AddTo(this.Disposable);
            ResidenceValidation = ResidenceSelected.SetValidateNotifyError((val) => string.IsNullOrEmpty(val) ? "居住地が選択されていません" : null).AddTo(this.Disposable);
            WhereValidation = WhereSelected.SetValidateNotifyError((val) => string.IsNullOrEmpty(val) ? "どこで知ったのかが選択されていません" : null).AddTo(this.Disposable);
            AccessValidation = AccessSelected.SetValidateNotifyError((val) => string.IsNullOrEmpty(val) ? "どのように来たのかが選択されていません" : null).AddTo(this.Disposable);

            AgeSelected.AddTo(this.Disposable);
            MemberSelected.AddTo(this.Disposable);
            ResidenceSelected.AddTo(this.Disposable);
            WhereSelected.AddTo(this.Disposable);
            AccessSelected.AddTo(this.Disposable);

            SubmitCommand = new[]
            {
                AgeValidation.ObserveHasErrors,
                MemberValidation.ObserveHasErrors,
                ResidenceValidation.ObserveHasErrors,
                WhereValidation.ObserveHasErrors,
                AccessValidation.ObserveHasErrors,
            }
                .CombineLatestValuesAreAllFalse()
                .ToReactiveCommand()
                .AddTo(this.Disposable);

            SubmitCommand.Subscribe(async () =>
            {
                await Submit();
                await SaveUserProfile();
                await _navigationService.NavigateAsync("/AppNavigationRootPage/NavigationPage/HomePage");
            }).AddTo(this.Disposable);

            SkipCommand = new DelegateCommand(async () =>
            {
                SetValueSecret();
                await Submit();
                await SaveUserProfile();
                await _navigationService.NavigateAsync("/AppNavigationRootPage/NavigationPage/HomePage");
            });

            TermsOfUseCommand = new DelegateCommand(async () =>
            {
                await _webService.OpenUri(constUrls.TermsOfUseUrl);
            });
        }

        private void SetValueSecret()
        {
            AgeSelected.Value = SECRET_STR;
            MemberSelected.Value = SECRET_STR;
            ResidenceSelected.Value = SECRET_STR;
            WhereSelected.Value = SECRET_STR;
            AccessSelected.Value = SECRET_STR;
        }

        private async Task Submit()
        {
            await _analyticsService.SendUserAttributes(
                    AGE[AgeSelected.Value],
                    MEMBER[MemberSelected.Value],
                    RESIDENCE[ResidenceSelected.Value],
                    WHERE[WhereSelected.Value],
                    ACCESS[AccessSelected.Value]);
            await _configService.SetEnqueteSentAsync(true);
        }

        private async Task SaveUserProfile()
        {
            try
            {
                var guid = await _analyticsService.GetUserId();
                await _configService.SetUserIdAsync(guid);
            }
            catch(Exception e)
            {
                
            }
        }
    }
}
