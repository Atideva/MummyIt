namespace AudioSystem
{
    public static class Audio
    {
        public static void Play(AudioData d) => AudioManager.Instance.PlaySound(d);

        public static void PlayMusic(AudioData d, float volume = 1f, float transition = 1f) =>
            AudioManager.Instance.PlayMusic(d, volume, transition);
    }
}