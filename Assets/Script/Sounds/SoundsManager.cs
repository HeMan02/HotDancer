using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource m_AudioSource;

    [Header("Audio Clip")]
    [SerializeField] AudioClip m_AudioClipBullet;
    [SerializeField] AudioClip m_AudioClipGranadeExplosion;
    [SerializeField] AudioClip m_AudioClipReloading;
    [SerializeField] AudioClip m_AudioClipChangePower;
    [SerializeField] AudioClip m_AudioClipDamagePlayer;
    [SerializeField] AudioClip m_AudioClipDamageEnemy;
    [SerializeField] AudioClip m_AudioClipLevelMusic;

    // Start is called before the first frame update
    void Start()
    {
        PowersManager.Instance.attackAction += BulletSound;
        Mediator.Instance.RegisterAction(GranadeExplosionSound, IEntity.TypeEvents.ExplosionGranade);
        Mediator.Instance.RegisterAction(ReloadSound, IEntity.TypeEvents.Reload);
        Mediator.Instance.RegisterAction(ChangePowerSound, IEntity.TypeEvents.ChangePower);
        Mediator.Instance.RegisterAction(DamagePlayerSound, IEntity.TypeEvents.Life);
        Mediator.Instance.RegisterAction(DamageEnemySound, IEntity.TypeEvents.KillEnemy);
        m_AudioSource.clip = m_AudioClipLevelMusic;
        m_AudioSource.loop = true;
        m_AudioSource.Play();
        m_AudioSource.volume = 0.1f;
    }

    public void BulletSound(bool value)
    {
        if (value)
            m_AudioSource.PlayOneShot(m_AudioClipBullet);
    }

    public void GranadeExplosionSound(object value)
    {
        if ((bool)value)
            m_AudioSource.PlayOneShot(m_AudioClipGranadeExplosion);
    }

    public void ReloadSound(object value)
    {
        if ((bool)value)
            m_AudioSource.PlayOneShot(m_AudioClipReloading);
    }

    public void ChangePowerSound(object value)
    {
        m_AudioSource.PlayOneShot(m_AudioClipChangePower);
    }

    public void DamagePlayerSound(object value)
    {
        m_AudioSource.PlayOneShot(m_AudioClipDamagePlayer);
    }

    public void DamageEnemySound(object value)
    {
        m_AudioSource.PlayOneShot(m_AudioClipDamageEnemy);
    }
}
