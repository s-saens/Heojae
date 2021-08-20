using UnityEngine;
using UniRx;

public class LinkFactory : MonoBehaviour
{
    LinkPool linkPool = null;

    public Link linkPrefab;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        linkPool = new LinkPool(linkPrefab, this.transform);
    }

    public Link PoolLink()
    {
        Link link = linkPool.Rent();

        return link;
    }

    public void RemoveLink(Link link)
    {
        linkPool.Return(link);
    }
}