using UnityEngine;
using UniRx.Toolkit;

public class LinkPool : ObjectPool<Link>
{
    readonly Link linkPrefab;
    readonly Transform hierarchyParent;

    public LinkPool(Link prefab, Transform hierarchyParent)
    {
        this.linkPrefab = prefab;
        this.hierarchyParent = hierarchyParent;
    }

    protected override Link CreateInstance()
    {
        var link = GameObject.Instantiate<Link>(linkPrefab);
        link.transform.SetParent(hierarchyParent);

        return link;
    }
}
