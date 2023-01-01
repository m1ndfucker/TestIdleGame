using SaveSystem;

public interface ISavableEntity {
	public void Save(ref GameData gameData);
	public void Load(GameData gameData);
}
