using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
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


    void Start()
    {
        // Добавление аудиоклипов в менеджеры звуков
        AudioManager.instance.AddMusicClip("BackgroundMusic", backgroundMusic);
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
        AudioManager.instance.AddEffectClip("BirdDeathEffect", birdDeathEffect);

        // Воспроизведение фоновой музыки
        AudioManager.instance.PlayMusic("BackgroundMusic");
    }

    public void OnButtonClick()
    {
        AudioManager.instance.PlayEffect("ButtonClick");
    }
}
