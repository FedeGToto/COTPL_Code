using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class TagsManager : MonoBehaviour
{
    [SerializeField] private TagSO[] startingTags; 
    private List<TagSO> tags;

    private void Start()
    {
        foreach (TagSO tag in startingTags)
        {
            AddTag(tag);
        }
    }

    public void AddTag(TagSO tag)
    {
        tags ??= new List<TagSO>();

        if (SearchTag(tag.Name) != null)
            tags.Add(tag);
        else
            print("This entity already has this tag");
    }

    public void RemoveTag(TagSO tag)
    {
        TagSO selectedTag = SearchTag(tag.Name);

        if (selectedTag != null)
        {
            tags.Remove(selectedTag);
        }
        else
            print("This entity doesn't have this tag");
    }

    public bool HasTag(string tagId)
    {
        foreach (TagSO tag in tags)
        {
            if (tag.name == tagId)
                return true;
        }

        return false;
    }

    private TagSO SearchTag(string tagId)
    {
        foreach (TagSO tag in tags)
        {
            if (tag.Name == tagId) return tag;
        }
        return null;
    }
}
