using System;
using WixSharp;

namespace Carrot.Install
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Project project =
            new Project("Bamboo Carrot Build Monitor",
                new Dir(new Id("INSTALL_DIR"), @"%ProgramFiles%\Bamboo Carrot\Bamboo Carrot Build Monitor",
                    new File(new Id("Carrot.exe"), @"..\Carrot\bin\Carrot.exe",
                        new FilePermission("Everyone", GenericPermission.Read | GenericPermission.Execute),
                        new FilePermission("Administrator", GenericPermission.Read | GenericPermission.Execute)),
                    new File(new Id("Carrot.exe.config"), @"..\Carrot\bin\Carrot.exe.config"),
                    new File(new Id("Carrot.ComposedApplication.dll"), @"..\Carrot\bin\Carrot.ComposedApplication.dll"),
                    new File(new Id("Carrot.Carrot.Contracts.dll"), @"..\Carrot\bin\Carrot.Contracts.dll")),
                new Dir(@"%AppData%\Microsoft\Windows\Start Menu\Programs\Startup",
                    new ExeFileShortcut(new Id("autorunShortcut"), "Build Monitor", "[INSTALL_DIR]Carrot.exe", String.Empty)))
            {
                Actions = new WixSharp.Action[]
                {
                    new InstalledFileAction("Carrot.exe", "", Return.asyncNoWait, When.After, Step.InstallFinalize, Condition.NOT_Installed),
                },
                GUID = new Guid("6F4B779B-EBD4-4E46-8EBE-5BE7CED8C3D8"),
                UI = WUI.WixUI_ProgressOnly
            };
            project.ControlPanelInfo.Manufacturer = "Bamboo Carrot";
            project.ControlPanelInfo.ProductIcon = "install_icon.ico";
            project.ControlPanelInfo.Comments = "Bamboo Carrot Build Monitor";
            Compiler.BuildMsi(project);
        }
    }
}
