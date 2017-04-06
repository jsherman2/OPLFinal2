// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open System.Windows.Forms
open System.Drawing
open System.Runtime.InteropServices


[<DllImport("kernel32.dll")>] extern System.IntPtr GetConsoleWindow()
[<DllImport("user32.dll")>] extern bool ShowWindow(System.IntPtr,  int)

let h = GetConsoleWindow()
ShowWindow(h, 0) |> ignore

let form = new Form(Width = 750, Height = 750)
form.Visible <- true
form.Text <- "Learning with numbers"

let label = new Label( Width = 300)
label.Text <- "Select a learning module below to get started"

form.Controls.Add(label)

let binbtn = new Button(Text = "Binary Lesson", Width = 350, Height = 100)
binbtn.Location <- new Point(175, 150)
let hexbtn = new Button(Text = "Hex Lesson", Width = 350, Height = 100)
hexbtn.Location <- new Point(175, 300)
let octalbtn = new Button(Text = "Octal Lesson", Width = 350, Height = 100)
octalbtn.Location <- new Point(175, 450)

form.Controls.Add(binbtn)
form.Controls.Add(hexbtn)
form.Controls.Add(octalbtn)

[<STAThread>]
Application.Run(form)
