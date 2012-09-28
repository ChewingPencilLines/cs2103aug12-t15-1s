using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToDo
{
    public class CommandList
    {
        public List<string> addList;
        public List<string> deleteList;
        public List<string> updateList;
        public List<string> undoList;
        public List<string> redoList;

        public CommandList()
        {
            addList = new List<string>();
            deleteList = new List<string>();
            updateList = new List<string>();
            undoList = new List<string>();
            redoList = new List<string>();
        }
    }

    public enum Commands { ADD = 1, DELETE, UPDATE, UNDO, REDO, NONE };

    public class SettingsManager
    {
        private CommandList commandList;

        #region CommandEnumManager

        public Commands StringToCommand(string commandString)
        {
            switch (commandString)
            {
                case "ADD":
                    return Commands.ADD;
                case "DELETE":
                    return Commands.DELETE;
                case "UPDATE":
                    return Commands.UPDATE;
                case "UNDO":
                    return Commands.UNDO;
                case "REDO":
                    return Commands.REDO;
            }

            return Commands.NONE;
        }

        public string CommandToString(Commands commandInput)
        {
            switch (commandInput)
            {
                case Commands.ADD:
                    return "ADD";
                case Commands.DELETE:
                    return "DELETE";
                case Commands.UPDATE:
                    return "UPDATE";
                case Commands.UNDO:
                    return "UNDO";
                case Commands.REDO:
                    return "REDO";
            }

            return "NONE";
        }

        #endregion

        public SettingsManager()
        {
            commandList = new CommandList();
            OpenFile("TEST.xml");
        }

        public void AddCommand(string newCommand, Commands commandType)
        {
            switch (commandType)
            {
                case Commands.ADD:
                    commandList.addList.Add(newCommand);
                    break;
                case Commands.DELETE:
                    commandList.deleteList.Add(newCommand);
                    break;
                case Commands.UPDATE:
                    commandList.updateList.Add(newCommand);
                    break;
                case Commands.UNDO:
                    commandList.undoList.Add(newCommand);
                    break;
                case Commands.REDO:
                    commandList.redoList.Add(newCommand);
                    break;
            }

            WriteToFile("TEST.xml");
        }

        public void RemoveCommand(string commandToRemove, Commands commandType)
        {
            switch (commandType)
            {
                case Commands.ADD:
                    commandList.addList.Remove(commandToRemove);
                    break;
                case Commands.DELETE:
                    commandList.deleteList.Remove(commandToRemove);
                    break;
                case Commands.UPDATE:
                    commandList.updateList.Remove(commandToRemove);
                    break;
                case Commands.UNDO:
                    commandList.undoList.Remove(commandToRemove);
                    break;
                case Commands.REDO:
                    commandList.redoList.Remove(commandToRemove);
                    break;
            }

            WriteToFile("TEST.xml");
        }

        public List<string> GetCommand(Commands commandType)
        {
            List<string> getCommands = new List<string>();
            switch (commandType)
            {
                case Commands.ADD:
                    getCommands = commandList.addList;
                    break;
                case Commands.DELETE:
                    getCommands = commandList.deleteList;
                    break;
                case Commands.UPDATE:
                    getCommands = commandList.updateList;
                    break;
                case Commands.UNDO:
                    getCommands = commandList.undoList;
                    break;
                case Commands.REDO:
                    getCommands = commandList.redoList;
                    break;
            }

            return getCommands;
        }

        //Just a Test Function
        public void SetUpCommands()
        {
            AddCommand("+", Commands.ADD);
            AddCommand("-", Commands.DELETE);
        }

        public Commands CheckIfCommandExists(string userCommand)
        {
            foreach (string compare in commandList.addList)
                if (userCommand == compare)
                    return Commands.ADD;
            foreach (string compare in commandList.deleteList)
                if (userCommand == compare)
                    return Commands.DELETE;
            foreach (string compare in commandList.updateList)
                if (userCommand == compare)
                    return Commands.UPDATE;
            foreach (string compare in commandList.undoList)
                if (userCommand == compare)
                    return Commands.UNDO;
            foreach (string compare in commandList.redoList)
                if (userCommand == compare)
                    return Commands.REDO;

            return Commands.NONE;
        }

        public void WriteToFile(string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(CommandList));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                fileName);
            writer.Serialize(file, commandList);
            file.Close();
        }

        public void OpenFile(string fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(CommandList));

            System.IO.StreamReader file = new System.IO.StreamReader(
                fileName);
            commandList = (CommandList)writer.Deserialize(file);

            file.Close();
        }

    }
}
