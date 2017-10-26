using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using TonpeiFes.MobileCore.Services;
using TonpeiFes.MobileCore.Configurations;
using Prism.Events;
using TonpeiFes.MobileCore.Models.EventArgs;
using System.Reactive.Linq;
using TonpeiFes.MobileCore.Extensions;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class VoteAnnouncePageViewModel : ViewModelBase
    {
        public string Title { get; } = "投票";

        public ReactiveCommand GoMrVoteCommand { get; }
        public ReactiveCommand GoMsVoteCommand { get; }
        public ReactiveCommand GoT1VoteCommand { get; }

        public ReactiveProperty<string> MrMsErrorText { get; }
        public ReactiveProperty<string> T1ErrorText { get; }

        private ReactiveProperty<DateTimeOffset> MsContestOpenValidate;
        private ReactiveProperty<DateTimeOffset> IsFestaOpeningValidate;
        private ReactiveProperty<DateTimeOffset> ReactiveCurrentTimeMs;
        private ReactiveProperty<DateTimeOffset> ReactiveCurrentTimeT1;

        private readonly DateTimeOffset StartMsContest = new DateTimeOffset(2017, 11, 4, 16, 0, 0, new TimeSpan(9, 0, 0));
        private readonly DateTimeOffset EndMsContest = new DateTimeOffset(2017, 11, 4, 20, 0, 0, new TimeSpan(9, 0, 0));
        private readonly DateTimeOffset StartT1 = new DateTimeOffset(2017, 11, 3, 0, 0, 0, new TimeSpan(9, 0, 0));
        private readonly DateTimeOffset EndT1 = new DateTimeOffset(2017, 11, 6, 0, 0, 0, new TimeSpan(9, 0, 0));

        public VoteAnnouncePageViewModel(IEventAggregator eventAggregator, IOpenWebPageService webService, IConstUrls constUrls)
        {
            ReactiveCurrentTimeMs = new ReactiveProperty<DateTimeOffset>(GetCurrentDateTimeOffset()).AddTo(this.Disposable);
            ReactiveCurrentTimeT1 = new ReactiveProperty<DateTimeOffset>(GetCurrentDateTimeOffset()).AddTo(this.Disposable);

            MsContestOpenValidate = ReactiveCurrentTimeMs
                .SetValidateNotifyError((time) =>
            {
                var inSession = (time > StartMsContest) && (time < EndMsContest);
                return inSession ? null : "開催期間外です";
            }).AddTo(this.Disposable);

            IsFestaOpeningValidate = ReactiveCurrentTimeT1
                .SetValidateNotifyError((time) =>
            {
                var inSession = (time > StartT1) && (time < EndT1);
                return inSession ? null : "開催期間外です";
            }).AddTo(this.Disposable);

            MrMsErrorText = MsContestOpenValidate
                .ObserveErrorChanged
                .Select(x => x?.OfType<string>().FirstOrDefault() ?? "")
                .ToReactiveProperty()
                .AddTo(this.Disposable);

            T1ErrorText = IsFestaOpeningValidate
                .ObserveErrorChanged
                .Select(x => x?.OfType<string>().FirstOrDefault() ?? "")
                .ToReactiveProperty()
                .AddTo(this.Disposable);
            
            GoMrVoteCommand = MsContestOpenValidate
                .ObserveHasErrors
                .Select(x => !x)
                .ToReactiveCommand()
                .AddTo(this.Disposable);

            GoMsVoteCommand = MsContestOpenValidate
                .ObserveHasErrors
                .Select(x => !x)
                .ToReactiveCommand()
                .AddTo(this.Disposable);

            GoT1VoteCommand = IsFestaOpeningValidate
                .ObserveHasErrors
                .Select(x => !x)
                .ToReactiveCommand()
                .AddTo(this.Disposable);

            GoMrVoteCommand.Subscribe(async () =>
            {
                await webService.OpenUri(constUrls.MrContestVoteUrl);
            }).AddTo(this.Disposable);

            GoMsVoteCommand.Subscribe(async () =>
            {
                await webService.OpenUri(constUrls.MsContestVoteUrl);
            }).AddTo(this.Disposable);

            GoT1VoteCommand.Subscribe(async () =>
            {
                await webService.OpenUri(constUrls.T1VoteUrl);
            }).AddTo(this.Disposable);

            eventAggregator.GetEvent<TabbedPageOpendEvent>().Subscribe((open) =>
            {
                if (open.Name != nameof(VoteAnnouncePageViewModel).GetViewNameFromRule()) return;
                ReactiveCurrentTimeMs.Value = GetCurrentDateTimeOffset();
                ReactiveCurrentTimeT1.Value = GetCurrentDateTimeOffset();
            }).AddTo(this.Disposable);
        }

        private DateTimeOffset GetCurrentDateTimeOffset()
        {
            return new DateTimeOffset(DateTimeOffset.UtcNow.UtcTicks + new TimeSpan(9, 0, 0).Ticks, new TimeSpan(9, 0, 0));
        }
    }
}
