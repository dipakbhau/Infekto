using Godot;
using System;

public class Joystick : TouchScreenButton
{
	private void Print(object o)
	{
		GD.Print(o.ToString());
	}


	private Sprite outer;
	private TouchScreenButton inner;

	private Vector2 offset = new Vector2(100,100);
	private float radius = 100;



	public override void _Ready()
	{
		outer = GetParent<Sprite>();
		inner = this;
		Print(outer.Name);
		Print(inner.Name);
	}

	public override void _Process(float d)
	{
		if (!inner.IsPressed())
		{
			Vector2 dif = (new Vector2(0,0) - offset) - Position;
			Position += dif * 25 * d;
		}
	}

	public override void _Input(InputEvent e)
	{
		if(e is InputEventScreenTouch touch && inner.IsPressed())
		{
			setJoyPosition(touch.Position);
			Print(touch.Position);
		}

		if(e is InputEventScreenDrag drag && inner.IsPressed())
		{
			setJoyPosition(drag.Position);
			Print(drag.Position);
		}
	}

	private Vector2 getJoyPosition()
	{
		return Position + offset;
	}

	private void setJoyPosition(Vector2 pos)
	{
		GlobalPosition = pos - offset * GlobalScale;

		if(getJoyPosition().Length() > radius)
		{
			Position = getJoyPosition().Normalized() * radius - offset;
		}
	}
}



