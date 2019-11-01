using ActivityChain.Link;

namespace ActivityChain
{
    public interface IKeyProvider<TChainObj> where TChainObj : ISourceItem
    {
        string GetKey<TFunction>() where TFunction : IFunc<TChainObj>;

        string CreateKey<TFunction>() where TFunction : IFunc<TChainObj>;
    }
}