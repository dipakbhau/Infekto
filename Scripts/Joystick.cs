using Godot;
using System;

public class Joystick : TouchScreenButton
{
	private void Print(object o)
	{
		GD.Print(o.ToString());
	}

	private float valueLimit = 10f;

	private Vector2 offset = new Vector2(100, 100);
	[Export]
	public float radius = 100;

	[Export]
	public float sensitivity = 35f;

	private int touchCount = -1;

	public override void _Process(float d)
	{
		if (touchCount == -1)
		{
			Vector2 dif = (new Vector2(0, 0) - offset) - Position;
			Position += dif * sensitivity * d;
		}
	}

	public override void _Input(InputEvent e)
	{
		if (e is InputEventScreenTouch touch && IsPressed())
		{
			touchCount = touch.Index;
			GlobalPosition = touch.Position - offset * GlobalScale;
		}

		else if (e is InputEventScreenDrag drag && IsPressed())
		{
			touchCount = drag.Index;
			GlobalPosition = drag.Position - offset * GlobalScale;
		}

		if (getJoyPosition().Length() > radius)
		{
			Position = getJoyPosition().Normalized() * radius - offset;
		}

		if (!IsPressed() && touchCount == 0)
		{
			touchCount = -1;
		}
	}
	private Vector2 getJoyPosition()
	{
		return Position + offset;
	}
	public Vector2 get()
	{
		Vector2 pos = getJoyPosition().Normalized();
		if (getJoyPosition().Length() > valueLimit)
		{
			return pos;          
		}
		return Vector2.Zero;
	}
}



