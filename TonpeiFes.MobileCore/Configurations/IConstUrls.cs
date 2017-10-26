using System;
namespace TonpeiFes.MobileCore.Configurations
{
    public interface IConstUrls
    {
        // Vote
        string MrContestVoteUrl { get; }
        string MsContestVoteUrl { get; }
        string T1VoteUrl { get; }

        // Others
        string TermsOfUseUrl { get; }
        string LicenseUrl { get; }
    }
}
