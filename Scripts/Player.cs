using Godot;
using System;

public class Player : Spatial
{
	private void Print(object o)
	{
		GD.Print(o);
	}

    private Joystick input;

    public override void _Process(float delta)
    {
        try
        {
            input = (Joystick)GetParent().GetChild(0).GetNode("Control/outer/inner");
            Print(input.get());
        }
        catch (Exception e)
        {
            Print(e.Message);
        }
    }
}
