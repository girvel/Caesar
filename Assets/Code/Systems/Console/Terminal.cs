﻿using System.Collections.Generic;
using System.Linq;
using BashDotNet;
using Code.Common;
using Code.Systems.Input;
using Code.Systems.Net;
using Code.Systems.Placing;
using Code.Systems.Selection;
using Province.Vector;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Systems.Console
{
    public class Terminal : SingletonBehaviour<Terminal>, IKeyboardReader
    {
        public GameObject TerminalContainer;
        public Text TerminalText;
        
        
        
        public bool Enabled
        {
            get { return TerminalContainer.activeInHierarchy; }
            set
            {
                TerminalContainer.SetActive(value);
                if (value)
                {
                    if (!Keyboard.Current.Readers.Contains(this))
                        Keyboard.Current.Readers.Add(this);
                }
                else 
                {
                    Keyboard.Current.Readers.Remove(this);
                }
            }
        }

        public string Content
        {
            get { return TerminalText.text; }
            private set { TerminalText.text = value; }
        }

        public string CurrentCommand { get; private set; }
        
        
        
        public Library CommandLibrary { get; set; }

        private static Vector Selection
        {
            get { return SelectionSystem.Current.Selection.GetComponent<PositionComponent>().Position; }
        }

        protected override void Awake()
        {
            base.Awake();
            
            CommandLibrary 
                = new Library(
                    1,
                    new Command(
                        "help",
                        new string[0],
                        new Option[0],
                        (args, options) =>
                        {
                            foreach (var command in CommandLibrary.Commands)
                            {
                                WriteOutput(
                                    command.Name 
                                    + command.PositionalArguments.Aggregate("", (sum, a) => sum + " <" + a + ">") 
                                    + command.Options.Aggregate("", 
                                        (sum, o) => sum 
                                                    + " <-" + o.ShortName 
                                                    + "/--" + o.LongName 
                                                    + " [" + o.Name + "]>")
                                    + "\n");
                            }
                        }),
                    new Command(
                        "upgrade",
                        new string[0],
                        new[]
                        {
                            new Option("building", "building", 'b'), 
                        },
                        (args, options) =>
                        {
                            WriteOutput(
                                ClientBehaviour.NetManager.UpgradeBuilding(
                                    Selection,
                                    options["building"] ?? "Wooden house")
                                    ? "Upgraded successfully.\n"
                                    : "Upgrade is impossible.\n");
                        }),
                    new Command(
                        "move",
                        new[] {"delta"},
                        new Option[0],
                        (args, options) =>
                        {
                            var from = Selection;

                            Vector delta;
                            if (!Vector.TryParse(args["delta"], out delta))
                            {
                                WriteOutput("First argument \"delta\" should be of type Vector.\n");
                                return;
                            }

                            WriteOutput(
                                ClientBehaviour.NetManager.Move(from, from + delta)
                                    ? "Movement began successfully.\n"
                                    : "Something went wrong.\n");
                        }),
                    new Command(
                        "research",
                        new[] {"name"},
                        new Option[0],
                        (args, options) =>
                        {
                            WriteOutput(
                                ClientBehaviour.NetManager.BeginResearch(args["name"])
                                    ? "Research started successfully\n"
                                    : "You can not research this\n");
                        }),
                    new Command(
                        "count_researches",
                        new string[0],
                        new Option[0],
                        (args, options) =>    
                        {
                            WriteOutput(
                                ClientBehaviour.NetManager.GetTechnologiesCount().ToString() + "\n");
                        }),
                    new Command(
                        "clear",
                        new string[0],
                        new Option[0],
                        (args, options) =>
                        {
                            TerminalText.text = "";
                        }),
                    new Command(
                        "attack",
                        new[] {"delta"},
                        new Option[0],
                        (args, options) =>
                        {
                            Vector delta;
                            WriteOutput(
                                Vector.TryParse(args["delta"], out delta)
                                    ? ClientBehaviour.NetManager.Attack(Selection, Selection + delta)
                                        ? "Attacked sucessfully\n"
                                        : "You can not attack\n"
                                    : "First argument \"delta\" should be of type Vector.\n");
                        }));
        }

        bool IKeyboardReader.KeyIsPressed()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Backspace))
            {
                CurrentCommand = CurrentCommand.Substring(0, CurrentCommand.Length - 1);
                Content = Content.Substring(0, Content.Length - 1);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                WriteCommand("\n");
            }
            else
            {
                WriteCommand(UnityEngine.Input.inputString);
            }

            return true;
        }


        
        private void WriteCommand(string text)
        {
            IEnumerable<string> commands = (CurrentCommand + text).Split('\n');

            Content += text;
            CurrentCommand = commands.Last();
            commands = commands.Except(new[] {commands.Last()});

            foreach (var command in commands)
            {
                if (!CommandLibrary.TryExecute(command))
                {
                    WriteOutput("Wrong command\n");
                }
            }
        }

        private void WriteOutput(string text)
        {
            Content = Content.Substring(0, Content.Length - CurrentCommand.Length);
            Content += text;
            Content += CurrentCommand;
        }
    }
}