using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
 

    public SkeletonAnimation anim;
    public GameObject buttonPrefab;
    public GameObject contents;
   

    private void Start()
    {
        // contents.GetComponent<RectTransform>().sizeDelta = new Vector2(300, anim.skeleton.Data.Animations.Count);
        for(int i = 0; i < anim.skeleton.Data.Animations.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, contents.transform);
            button.GetComponentInChildren<Text>().text = anim.skeleton.Data.Animations.Items[i].Name;
            button.GetComponent<AnimationDemo>().anim = anim;
            button.GetComponent<AnimationDemo>().index = i;
        }
    }

    public void ChangeAnim(SkeletonDataAsset skeletonDataAsset)
    {

        foreach (Transform child in contents.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        anim.skeletonDataAsset = skeletonDataAsset;
        anim.Initialize(overwrite: true);


        for (int i = 0; i < anim.skeleton.Data.Animations.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, contents.transform);
            button.GetComponentInChildren<Text>().text = anim.skeleton.Data.Animations.Items[i].Name;
            button.GetComponent<AnimationDemo>().anim = anim;
            button.GetComponent<AnimationDemo>().index = i;
        }
    }


}
