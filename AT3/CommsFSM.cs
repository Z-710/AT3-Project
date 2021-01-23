using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <Acknowledgments>
/// How to create a static class
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
/// How to build a finite state machine
/// https://stackoverflow.com/questions/5923767/simple-state-machine-example-in-c
/// </Acknowledgments>
namespace AT3
{
    class CommsFSM
    {
        public enum ProcessState
        {
            NotConnected,
            Listening,
            ContactCalled,
            UserConnected,
            ContactConnected
        }

        public enum Command
        {
            Start,
            ContactConnects,
            UserConnects, 
            UserAnswers,
            ContactDisconnects,
            UserDisconnects
        }
        // Remember the current state
        private static ProcessState state = ProcessState.NotConnected;
        public static ProcessState GetCurrentState()
        {
            return state;
        }
        public static ProcessState SetNextState(Command cmd)
        {
            switch (state)
            {
                case ProcessState.NotConnected:
                    if (cmd == Command.Start) state = ProcessState.Listening;
                    break;
                case ProcessState.Listening:
                    if (cmd == Command.UserConnects) state = ProcessState.UserConnected;
                    if (cmd == Command.ContactConnects) state = ProcessState.ContactCalled;
                    break;
                case ProcessState.ContactCalled:
                    if (cmd == Command.UserAnswers) state = ProcessState.ContactConnected;
                    if (cmd == Command.ContactDisconnects) state = ProcessState.NotConnected;
                    break;
                case ProcessState.UserConnected:
                    if (cmd == Command.UserDisconnects) state = ProcessState.NotConnected;
                    break;
                case ProcessState.ContactConnected:
                    if (cmd == Command.ContactDisconnects) state = ProcessState.NotConnected;
                    if (cmd == Command.UserDisconnects) state = ProcessState.NotConnected;
                    break;
                default:
                    break;

                    
                   
            }
            return state;
        }
    }
}
