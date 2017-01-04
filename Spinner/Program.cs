using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Threading;

namespace Spinner
{
    class Program
    {
        static string[] Colors = new[]
        {
            "red",
            "green",
            "yellow",
            "blue",
        };

        static string[] Limbs = new[]
        {
            "left hand",
            "right hand",
            "left foot",
            "right foot"
        };

        static string[] Actions = new[]
        {
            "and pat the mat",
            "and bark like a dog",
            "and pat your head 5 times",
            "and shake your ass",
            "and hop on one foot",
            "and play pat-a-cake with the mat",
            "and play rock paper scissors with the player next to you",
            "and take off a piece of clothing",
        };
        static void Main(string[] args)
        {
            var rand = new Random();
            var synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Volume = 100;

            var names = args;

            names = names.OrderBy(f => rand.Next()).ToArray();

            var nCounter = 0;
            var sleepMult = 1d;

            while(true)
            {
                var limb = Limbs[rand.Next(Limbs.Length)];
                var color = Colors[rand.Next(Colors.Length)];
                //1/3 of the spinner locations are purple action spots.
                var doAction = rand.NextDouble() > 2 / 3d;
                var name = names[nCounter++];
                var action = Actions[rand.Next(Actions.Length)];

                if (nCounter >= names.Length)
                {
                    sleepMult -= 0.1;
                    if (sleepMult <= 0.3)
                        sleepMult = 0.3;
                    nCounter = 0;
                }

                synth.Rate = -2;
                synth.Speak(name);
                Thread.Sleep(250);
                synth.Speak(limb);
                Thread.Sleep(250);
                synth.Rate = -7;
                synth.Speak(color);

                //twister spinners have purple 'action' spots that the person spinning is supposed to choose the location and an action for the player to perform.
                //we'll let Random decide the action as it regularly does, but tack on an action.
                
                if (doAction)
                {
                    synth.Rate = -3;
                    Thread.Sleep(250);
                    synth.Speak(name);
                }

                synth.Rate = -2;
                Thread.Sleep(250);
                synth.Speak(name);

                if (doAction)
                    Thread.Sleep((int)(7000 * sleepMult));

                Thread.Sleep((int)(10000 * sleepMult));
            }
        }
    }
}
