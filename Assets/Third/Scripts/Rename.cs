using UnityEngine;

public class Rename : PropertyAttribute
{
    public string Name { get; set; }

	public Rename(string _name)
	{
		Name = _name;
	}
}
