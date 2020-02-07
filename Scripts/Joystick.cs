using Godot;
using System;

public class Joystick : TouchScreenButton
{
	private void Print(object o)
	{
		GD.Print(o.ToString());
	}

	private float valueLimit = 10f;
	private Sprite outer;
	private TouchScreenButton inner;

	private Vector2 offset = new Vector2(100,100);
	[Export]
	public float radius = 100;

	[Export]
	public float sensitivity = 35f;

	public override void _Ready()
	{
		outer = GetParent<Sprite>();
		inner = this;
	}

	public override void _Process(float d)
	{
		if (!inner.IsPressed())
		{
			Vector2 dif = (new Vector2(0,0) - offset) - Position;
			Position += dif * sensitivity * d;
		}
		Print(get());
	}

	public override void _Input(InputEvent e)
	{
		if(e is InputEventScreenTouch touch)
		{
			setJoyPosition(touch.Position,touch.Index);
		}

		if(e is InputEventScreenDrag drag)
		{
			setJoyPosition(drag.Position,drag.Index);
		}
	}

	private Vector2 getJoyPosition()
	{
		return Position + offset;
	}

	private void setJoyPosition(Vector2 pos,int index)
	{
		if(index == 0 && inner.IsPressed())
		{
			GlobalPosition = pos - offset * GlobalScale;
		}
		if (getJoyPosition().Length() > radius)
		{
			Position = getJoyPosition().Normalized() * radius - offset;
		}
	}

	public Vector2 get()
	{
		if (getJoyPosition().Length() > valueLimit)
		{
			return getJoyPosition().Normalized();
		}
		return Vector2.Zero;
	}
}



