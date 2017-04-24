﻿// Learn more about F# at http://fsharp.org
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

let hexButtonClicked evArgs sender =
    // make new form and populate
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 750, Height = 750)
    form.Visible <- true
    form.Text <- "Hexadecimal lesson"

    let hexlabel = new Label(Width = 300, Height = 400, Location = new Point(225,0))

    hexlabel.Text <- "Hexadecimal – also known as hex or base 16 – is a system we can use to write and share numerical values. In that way it’s no different than the most famous of numeral systems (the one we use every day): decimal. Decimal is a base 10 number system (perfect for beings with 10 fingers), and it uses a collection of 10 unique digits, which can be combined to positionally represent numbers.
     \nHex, like decimal, combines a set of digits to create large numbers. It just so happens that hex uses a set of 16 unique digits. \n\nHex uses the standard 0-9, but it also incorporates six digits you wouldn’t usually expect to see creating numbers: A, B, C, D, E, and F.Hexadecimal is a base-16 number system. That means there are 16 possible digits used to represent numbers. 10 of the numerical values you’re probably used to seeing in decimal numbers: 0, 1, 2, 3, 4, 5, 6, 7, 8, and 9; those values still represent the same value you’re used to. The remaining six digits are represented by A, B, C, D, E, and F, which map out to values of 10, 11, 12, 13, 14, and 15.
     \n\n Try converting decimal numbers in the box below to see what the hex will be!"

    form.Controls.Add(hexlabel)

    let hexBox = new TextBox(Width = 100, Height = 50, Location = new Point(225,500))
    let hexLabel = new Label(Width = 100, Height = 50, Location = new Point(350,500))
    hexLabel.Text <- "0"

    let int2String (x: int) = string x
    let takeMod s = 
        match s with
    | 15 -> "F"
    | 14 -> "E"
    | 13 -> "D"
    | 12 -> "C"
    | 11 -> "B"
    | 10 -> "A"
    | _ -> int2String s

    let (^) l r = sprintf "%s%s" l r

    let rec hexconvert s = 
        match s with 
    | 0 -> ""
    | _ -> hexconvert (s / 16) ^  (takeMod (s % 16))

    let subHexBtn = new Button(Width = 100, Height = 50, Location = new Point(225, 555), Text = "Submit")
    let subButtonClicked evArgs sender =
        let convert = hexBox.Text |> int
        hexLabel.Text <- hexconvert convert
    ()

    subHexBtn.Click.Add(fun evArgs -> subButtonClicked evArgs hexbtn)

    form.Controls.Add(hexBox)
    form.Controls.Add(hexLabel)
    form.Controls.Add(subHexBtn)

    ()

hexbtn.Click.Add(fun evArgs -> hexButtonClicked evArgs hexbtn)

form.Controls.Add(binbtn)
form.Controls.Add(hexbtn)
form.Controls.Add(octalbtn)

[<STAThread>]
Application.Run(form)
