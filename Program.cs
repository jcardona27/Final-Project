using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await GameEngine.Instance.StartGameAsync();
    }
}