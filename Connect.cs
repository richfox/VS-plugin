using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;

namespace MyAddin
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        private const int TOOLWINDOW_INVISIBLE = 0;
        private const int TOOLWINDOW_VISIBLE = 1;

        private const string MY_COMMAND_NAME = "MyCommand";
        private const string MY_COMMAND_CAPTION = "My toolwindow";
        private const string MY_COMMAND_TOOLTIP = "Show the toolwindow of the add-in";

        private EnvDTE80.DTE2 applicationObject;
        private EnvDTE.AddIn addInInstance;

        private CommandBarButton myStandardCommandBarButton;
        private EnvDTE.Window myToolWindow;

        /// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
        public Connect()
        {
        }

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            try
            {
                applicationObject = (EnvDTE80.DTE2)application;
                addInInstance = (EnvDTE.AddIn)addInInst;

                switch (connectMode)
                {
                    case ext_ConnectMode.ext_cm_UISetup:

                        // Do nothing for this add-in with temporary user interface
                        break;

                    case ext_ConnectMode.ext_cm_Startup:

                        // The add-in was marked to load on startup
                        // Do nothing at this point because the IDE may not be fully initialized
                        // Visual Studio will call OnStartupComplete when fully initialized
                        break;

                    case ext_ConnectMode.ext_cm_AfterStartup:

                        // The add-in was loaded by hand after startup using the Add-In Manager
                        // Initialize it in the same way that when is loaded on startup
                        AddTemporaryUI();
                        break;
                }
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public void OnStartupComplete(ref System.Array custom)
        {
            AddTemporaryUI();
        }

        public void AddTemporaryUI()
        {
            const string VS_STANDARD_COMMANDBAR_NAME = "Standard";

            Command myCommand = null;
            CommandBar standardCommandBar = null;
            CommandBars commandBars = null;
            Microsoft.Win32.RegistryKey registryKey;

            object[] contextUIGuids = new object[] { };

            try
            {
                // Try to retrieve the command, just in case it was already created, ignoring the 
                // exception that would happen if the command was not created yet.
                try
                {
                    myCommand = applicationObject.Commands.Item(addInInstance.ProgID + "." + MY_COMMAND_NAME, -1);
                }
                catch
                {
                }

                // Add the command if it does not exist
                if (myCommand == null)
                {
                    myCommand = applicationObject.Commands.AddNamedCommand(addInInstance,
                       MY_COMMAND_NAME, MY_COMMAND_CAPTION, MY_COMMAND_TOOLTIP, true, 59, ref contextUIGuids,
                       (int)(vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled));
                }

                // Retrieve the collection of commandbars
                commandBars = (CommandBars)applicationObject.CommandBars;

                // Retrieve some built-in commandbars
                standardCommandBar = commandBars[VS_STANDARD_COMMANDBAR_NAME];

                // Add a button on the "Standard" toolbar
                myStandardCommandBarButton = (CommandBarButton)myCommand.AddControl(standardCommandBar,
                   standardCommandBar.Controls.Count + 1);

                // Change some button properties
                myStandardCommandBarButton.Caption = MY_COMMAND_CAPTION;
                myStandardCommandBarButton.BeginGroup = true;

                // Get if the toolwindow was visible when the add-in was unloaded last time to show it
                registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\MyToolWindow");
                if (registryKey != null)
                {
                    if ((int)registryKey.GetValue("MyToolwindowVisible") == TOOLWINDOW_VISIBLE)
                    {
                        ShowToolWindow();
                    }
                    registryKey.Close();
                }

            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
        {
            Microsoft.Win32.RegistryKey registryKey;
            int myToolWindowVisible;

            try
            {
                switch (RemoveMode)
                {
                    case ext_DisconnectMode.ext_dm_HostShutdown:
                    case ext_DisconnectMode.ext_dm_UserClosed:

                        if ((myStandardCommandBarButton != null))
                        {
                            myStandardCommandBarButton.Delete(true);
                        }


                        // Store in the Windows Registry if the toolwindow was visible when unloading the add-in
                        myToolWindowVisible = TOOLWINDOW_INVISIBLE;
                        if (myToolWindow != null)
                        {
                            if (myToolWindow.Visible)
                            {
                                myToolWindowVisible = TOOLWINDOW_VISIBLE;
                            }
                        }

                        registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\MyToolWindow");
                        registryKey.SetValue("MyToolwindowVisible", myToolWindowVisible);
                        registryKey.Close();

                        break;
                }
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public void OnBeginShutdown(ref System.Array custom)
        {
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;

            if ((executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault))
            {
                if (commandName == addInInstance.ProgID + "." + MY_COMMAND_NAME)
                {
                    handled = true;
                    ShowToolWindow();
                }
            }
        }

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == addInInstance.ProgID + "." + MY_COMMAND_NAME)
                {
                    status = (vsCommandStatus)(vsCommandStatus.vsCommandStatusEnabled |
                       vsCommandStatus.vsCommandStatusSupported);
                }
                else
                {
                    status = vsCommandStatus.vsCommandStatusUnsupported;
                }
            }
        }

        private void ShowToolWindow()
        {
            const string TOOLWINDOW_GUID = "{6CCD0EE9-20DB-4636-9149-665A958D8A9A}";

            EnvDTE80.Windows2 windows2;
            string assembly;
            object myUserControlObject = null;

            try
            {
                if (myToolWindow == null) // First time, create it
                {
                    windows2 = (EnvDTE80.Windows2)applicationObject.Windows;

                    assembly = System.Reflection.Assembly.GetExecutingAssembly().Location;

                    myToolWindow = windows2.CreateToolWindow2(addInInstance, assembly,
                       typeof(FileFind).FullName, "My toolwindow", TOOLWINDOW_GUID, ref myUserControlObject);


                    // Now you can pass values to the instance of the usercontrol
                    // myUserControl.Initialize(value1, value2)

                    InitUserControl(myUserControlObject);
                }

                myToolWindow.Visible = true;
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        private void InitUserControl(object obUserCtrl)
        {
            FileFind ff = obUserCtrl as FileFind;
            if (ff != null)
            {
                ff.Application = applicationObject;
                ff.AddInInstance = addInInstance;
                ff.FileFindWnd = myToolWindow;
            }
        }
    }
}