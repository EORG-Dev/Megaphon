using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Megaphon.Droid;
using Megaphon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoding = Android.Media.Encoding;

[assembly: Xamarin.Forms.Dependency(typeof(AudioStream))]
namespace Megaphon.Droid
{
    public class AudioStream : IAudioStream
    {
        private AudioRecord audioRecord;
        private AudioTrack audioTrack;

        public event EventHandler<byte[]> OnBroadcast;

        public async Task Start()
        {
            Task.Run(() =>
            {
                int bufferSize = AudioRecord.GetMinBufferSize(
                44100, // Sample rate
                ChannelIn.Mono,
                Encoding.Pcm16bit
            );

                audioRecord = new AudioRecord(
                    AudioSource.Mic,
                    44100,
                    ChannelIn.Mono,
                    Encoding.Pcm16bit,
                    bufferSize
                );

                audioTrack = new AudioTrack(
                    Stream.Music,
                    44100,
                    ChannelOut.Mono,
                    Encoding.Pcm16bit,
                    bufferSize,
                    AudioTrackMode.Stream
                );

                byte[] buffer = new byte[bufferSize];


                audioRecord.StartRecording();
                audioTrack.Play();

                while (audioRecord.RecordingState == RecordState.Recording)
                {
                    audioRecord.Read(buffer, 0, bufferSize);
                    audioTrack.Write(buffer, 0, bufferSize);
                }
            });
            
        }

        public void Stop()
        {
            audioRecord.Stop();
            audioRecord.Release();
            audioTrack.Stop();
            audioTrack.Release();
        }
    }
}