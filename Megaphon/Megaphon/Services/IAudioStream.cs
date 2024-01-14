using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Megaphon.Services
{
    public interface IAudioStream
    {
        Task Start();
        void Stop();
    }
}
