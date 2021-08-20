using UnityEngine;
using System.Collections.Generic;

public class Rope : MonoBehaviour
{
    public GameObject ropeObject;
    public GameObject stake;

    public int maxLinksCount;
    public List<Link> links;
    public LinkFactory linkFactory;

    public void Shoot(Vector2 initPos, Vector2 direction)
    {
        for(int i=0 ; i<maxLinksCount ; ++i)
        {
            links[i] = linkFactory.PoolLink();
            links[i].transform.rotation = Quaternion.Euler( 0, 0, (Mathf.Acos(initPos.y)) );
            
            if(links[i].coll != null)
            {
                break;
            }
        }
    }

    public void Cut()
    {
        foreach(var link in links)
        {
            linkFactory.RemoveLink(link);
        }
    }
}