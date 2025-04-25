using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace TeamRpg
    {
        public static class Music
        {
            private static IWavePlayer outputDevice;
            private static AudioFileReader audioFile;
            private static string currentFile = null;

      
       public static void PlayMusic(string filePath, int startSeconds = 0) // 음악 재생 메서드
        {
             if (IsPlaying && currentFile == filePath)// 이미 재생 중이면 다시 재생안함
                return;

                 StopMusic();

                audioFile = new AudioFileReader(filePath);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                audioFile.CurrentTime = TimeSpan.FromSeconds(startSeconds);
                outputDevice.Play();
                currentFile = filePath;//현재 파일 기억
        }

            public static void StopMusic()
            {
                if (outputDevice != null)
                {
                    outputDevice.Stop();
                    outputDevice.Dispose();
                    outputDevice = null;
                }

                if (audioFile != null)
                {
                    audioFile.Dispose();
                    audioFile = null;
                }
            }
        //재생상태확인
        public static bool IsPlaying =>outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing;

    }
}

