using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using BrowserPhoenix.Shared.Commands.Sync;

namespace BrowserPhoenix.Shared.Commands
{
    public static class CommandPortal
    {
        private static MessageQueue messageQueue = null;
        public static MessageQueue Open()
        {
            if (messageQueue == null)
            {
                messageQueue = new MessageQueue(".\\Private$\\phoenix");
                messageQueue.Formatter = new BinaryMessageFormatter();
            }
            return messageQueue;
        }

        public static void Send(Object command)
        {
            using (var queue = Open())
            {
                Message m = new Message();

                m.Formatter = queue.Formatter;
                m.Body = command;

                queue.Send(m);
            }
                
        }

        public static void ProcessNext()
        {
            // das hier kommt in eine init im static bla
            Dictionary<Type, CommandProcessor> commandProcessors = new Dictionary<Type, CommandProcessor>();
            commandProcessors.Add(typeof(CreateBuildingCommand), new CommandProcessor<CreateBuildingCommand>());
            commandProcessors.Add(typeof(GetPointsCommand), new CommandProcessor<GetPointsCommand>());
            commandProcessors.Add(typeof(CreateColonyCommand), new CommandProcessor<CreateColonyCommand>());
            commandProcessors.Add(typeof(LevelUpBuildingCommand), new CommandProcessor<LevelUpBuildingCommand>());
            commandProcessors.Add(typeof(RecalculateResourcesCommand), new CommandProcessor<RecalculateResourcesCommand>());
            commandProcessors.Add(typeof(CreateTroopCommand), new CommandProcessor<CreateTroopCommand>());
            //

            //das hier muss auch statis irgenwo sein
            using (var queue = Open())
            {
                Message m = queue.Receive();
                m.Formatter = queue.Formatter;

                var message = m.Body;
                //

                // das ist was hierhin gehört
                Type key = message.GetType();
                CommandProcessor processor = commandProcessors[key];

                //gibt dann das hier zurück
                processor.ProcessCommand(message);
            }
            
        }
    }
}