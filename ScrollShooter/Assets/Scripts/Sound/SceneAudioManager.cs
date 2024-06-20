using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;
    public AudioClip startMusic;
    public AudioClip buttonClickSound;
    public AudioClip gunshot;
    public AudioClip explosion;
    public AudioClip playerDeath;
    public AudioClip enemyDeath;
    public AudioClip enemyBulletExplosion;
    public AudioClip enemyLaserShot;
    public AudioClip playerJump;
    public AudioClip somersault;
    public AudioClip smoke;
    public AudioClip birdAtackEffect;
    public AudioClip birdDeathEffect;
    public AudioClip keyEffect;
    public AudioClip doorEffect;
    public AudioClip reloadEffect;
    public AudioClip healthEffect;



    void Start()
    {
        AudioManager.instance.AddMusicClip("StartMusic", startMusic);
        AudioManager.instance.AddMusicClip("GameMusic", gameMusic);
        AudioManager.instance.AddMusicClip("VictoryMusic", victoryMusic);
        AudioManager.instance.AddMusicClip("GameOverMusic", gameOverMusic);
        AudioManager.instance.AddEffectClip("ButtonClick", buttonClickSound);
        AudioManager.instance.AddEffectClip("GunShot", gunshot);
        AudioManager.instance.AddEffectClip("Explosion", explosion);
        AudioManager.instance.AddEffectClip("PlayerDeath", playerDeath);
        AudioManager.instance.AddEffectClip("EnemyDeath", enemyDeath);
        AudioManager.instance.AddEffectClip("EnemyBulletExplosion", enemyBulletExplosion);
        AudioManager.instance.AddEffectClip("EnemyLaserShot", enemyLaserShot);
        AudioManager.instance.AddEffectClip("PlayerJump", playerJump);
        AudioManager.instance.AddEffectClip("Somersault", somersault);
        AudioManager.instance.AddEffectClip("Smoke", smoke);
        AudioManager.instance.AddEffectClip("BirdEffect", birdAtackEffect);
        AudioManager.instance.AddEffectClip("DoorEffect", doorEffect);
        AudioManager.instance.AddEffectClip("KeyEffect", keyEffect);
        AudioManager.instance.AddEffectClip("BirdDeathEffect", birdDeathEffect);
        AudioManager.instance.AddEffectClip("ReloadEffect", reloadEffect);
        AudioManager.instance.AddEffectClip("HealthEffect", healthEffect);
    }

    public void OnButtonClick()
    {
        AudioManager.instance.PlayEffect("ButtonClick");
    }
}
