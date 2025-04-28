using System;

namespace CarterGames.Cart.Core
{
	[Flags]
	public enum FieldMarkerShowOption
	{
		None = 0,
		SceneContext = 1 << 0,
		PrefabContext = 1 << 1,
		GameObjectDisabled = 1 << 2,
		GameObjectEnabled = 1 << 3,
		All = SceneContext | PrefabContext | GameObjectDisabled | GameObjectEnabled
	}
}