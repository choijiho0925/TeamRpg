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
            // 육군 이등별인 경우, armytiger.wav 파일을 재생
            if (Game.Instance.player != null && Game.Instance.player.Job == "육군 이등별")
            {
                // 이미 육군 이등별 테마가 재생 중이면 또 재생하지 않음
                string armyTigerPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio/armytiger.wav");
                if (IsPlaying && currentFile == armyTigerPath)
                    return;

                StopMusic();

                // 파일 존재 여부 확인 (없으면 기본 음악 재생)
                if (System.IO.File.Exists(armyTigerPath))
                {
                    audioFile = new AudioFileReader(armyTigerPath);
                    outputDevice = new WaveOutEvent();
                    outputDevice.Init(audioFile);
                    audioFile.CurrentTime = TimeSpan.FromSeconds(startSeconds);
                    outputDevice.Play();
                    currentFile = armyTigerPath;
                    return; // 육군 이등별 테마 재생 후 종료
                }
            }

            // 기존 로직 - 일반 음악 재생
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
        public static bool IsPlaying => outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing;

    }
}

