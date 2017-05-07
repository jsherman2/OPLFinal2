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

let (^) l r = sprintf "%s%s" l r
let int2String (x: int) = string x

module String =
    /// Converts a string into a list of characters.
    let explode (s:string) =
        let rec loop n acc =
            if n = 0 then
                acc
            else
                loop (n-1) (s.[n-1] :: acc)
        loop s.Length []

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

    let decBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,415))
    let decLabel = new Label(Width = 100, Height = 50, Location = new Point(300,415))
    decLabel.Text <- "0"

    let hexBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,515))
    let hexLabel = new Label(Width = 100, Height = 50, Location = new Point(300,515))
    hexLabel.Text <- "0"
    
    let takeMod s = 
        match s with
    | 15 -> "F"
    | 14 -> "E"
    | 13 -> "D"
    | 12 -> "C"
    | 11 -> "B"
    | 10 -> "A"
    | _ -> int2String s
    
    let rec makehexarr list =
        match list with
    | [] -> []
    | ['0'] -> [0.0]
    | ['1'] -> [1.0]
    | ['2'] -> [2.0]
    | ['3'] -> [3.0]
    | ['4'] -> [4.0]
    | ['5'] -> [5.0]
    | ['6'] -> [6.0]
    | ['7'] -> [7.0]
    | ['8'] -> [8.0]
    | ['9'] -> [9.0]
    | ['A'] -> [10.0]
    | ['a'] -> [10.0]
    | ['B'] -> [11.0]
    | ['b'] -> [11.0]
    | ['C'] -> [12.0]
    | ['c'] -> [12.0]
    | ['D'] -> [13.0]
    | ['d'] -> [13.0]
    | ['E'] -> [14.0]
    | ['e'] -> [14.0]
    | ['F'] -> [15.0]
    | ['f'] -> [15.0]
    | head::tail -> (makehexarr [head])@(makehexarr tail)

    let rec hexconvert list n =
        match list with
    | [] -> 0.0
    | [x] -> ((x |> float)*(16.0**n))
    | head::tail -> (hexconvert [head] n) + (hexconvert tail (n-1.0))

    let rec hexsum list =
        match list with
    | [] -> 0.0
    | [x] -> x
    | head::tail -> head + (hexsum tail)

    let rec decconvert s =
        match s with
    | 0 -> ""
    | _ -> decconvert(s / 16) ^ takeMod (s % 16)

    let subDecBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 400), Text = "Dec -> Hex")
    let subDecButtonClicked evArgs sender =
        let convert = decBox.Text |> int
        let decresult = decconvert convert
        decLabel.Text <- decresult |> string //output result
        ()
    ()

    let subHexBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 500), Text = "Hex -> Dec")
    let subHexButtonClicked evArgs sender =
        let convert = String.explode (hexBox.Text |> string) //String.explode splits string into list of chars
        let hexarr = makehexarr convert //makes list of ints
        let hexresult = hexconvert hexarr (hexarr.Length-1 |> float) //result of summation of powers of 16
        hexLabel.Text <- hexresult |> string //output result
        ()
    ()

    subHexBtn.Click.Add(fun evArgs -> subHexButtonClicked evArgs hexbtn)
    subDecBtn.Click.Add(fun evArgs -> subDecButtonClicked evArgs hexbtn)
   
    form.Controls.Add(hexBox)
    form.Controls.Add(hexLabel)
    form.Controls.Add(subHexBtn)
    form.Controls.Add(decBox)
    form.Controls.Add(decLabel)
    form.Controls.Add(subDecBtn)

    ()

let octalButtonClicked evArgs sender =
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 650, Height = 650)
    form.Visible <- true
    form.Text <- "Octal lesson"

    let octallabel = new Label(Width = 550, Height = 400, Location = new Point(50,25))

    octallabel.Text <- "Octal is another number system with less symbols to use than our conventional number system.
    \nOctal is fancy for Base Eight meaning eight symbols are used to represent all the quantities. They are 0, 1, 2, 3, 4, 5, 6, and 7.
    \nWhen we count up one from the 7, we need a new placement to represent what we call 8 since an 8 doesn't exist in Octal. So, after 7 is 10.
    \nJust like how we used powers of ten in decimal and powers of two in binary, to determine the value of a number we will use powers of 8 since this is Base Eight. Consider the number 623 in base eight.
    \nIf we want to convert to decimal, we need to multiply each place in the number by a power of 8 and then add the answers togehter. The first place will start with 8^0 * 3, then 8^1 * 2, then 8^2 * 6. 
    \nThis gives us the statement 3 + 16 + 384 = 403. Thus, 623 in octal is 403 in decimal.
    \nTry converting decimal numbers in the box below to see what the octal will be!"

    form.Controls.Add(octallabel)

    let octalBox = new TextBox(Width = 100, Height = 50, Location = new Point(225,500))
    let octalLabel = new Label(Width = 100, Height = 50, Location = new Point(350,500))
    octalLabel.Text <- "0"

    let rec octalconvert s = 
        match s with
    | 0 -> ""
    | _ -> octalconvert(s / 8) ^ int2String (s % 8)

    let subOctalBtn = new Button(Width = 100, Height = 50, Location = new Point(225, 530), Text = "Dec -> Octal")
    let subButtonClicked evArgs sender =
        let convert = octalBox.Text |> int  //handle exceptions here
        octalLabel.Text <- octalconvert convert
    ()

    subOctalBtn.Click.Add(fun evArgs -> subButtonClicked evArgs hexbtn)

    form.Controls.Add(octalBox)
    form.Controls.Add(octalLabel)
    form.Controls.Add(subOctalBtn)

()

let binButtonClicked evArgs sender =
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 650, Height = 650)
    form.Visible <- true
    form.Text <- "Bin lesson"

    let binlabel = new Label(Width = 550, Height = 450, Location = new Point(50,25))

    binlabel.Text <- "Binary numbers are the most common alternative base to decimal. Ever heard that computers \"speak\" in ones and zeros? Well, they kind of do!
    \nSince digital logic relies on circuitry, it is helpful to represent numbers where each decimal only has two states: on or off--1 or 0. In fact, all digital computation can be reduced to zeros and ones.
    \nComputer hardware is designed to perform differently depeding on what parts of the hardware are recieving current and which aren't.
    \nIf you want to learn more about why binary is the basis for computer logic, check out Allan Turing and his \"Turing Machine\" or the digital logic behind TVs, microwaves, and digital clocks.
    \nNow let's talk about how to make sense of binary numbers. As in the introduction, we have to think about the base of binary numbers: 2. Here's an example to get started:
    \n 
    "

    form.Controls.Add(binlabel)

    let binBox = new TextBox(Width = 100, Height = 50, Location = new Point(225,500))
    let binLabel = new Label(Width = 100, Height = 50, Location = new Point(350,500))
    binLabel.Text <- "0"

    let rec binconvert s = 
        match s with
    | 0 -> ""
    | _ -> binconvert(s / 2) ^ int2String (s % 2)

    let subBinBtn = new Button(Width = 100, Height = 50, Location = new Point(225, 530), Text = "Dec -> Binary")
    let subButtonClicked evArgs sender =
        let convert = binBox.Text |> int //handle exceptions here
        binLabel.Text <- binconvert convert
    ()

    subBinBtn.Click.Add(fun evArgs -> subButtonClicked evArgs hexbtn)

    form.Controls.Add(binBox)
    form.Controls.Add(binLabel)
    form.Controls.Add(subBinBtn)

()


hexbtn.Click.Add(fun evArgs -> hexButtonClicked evArgs hexbtn)
binbtn.Click.Add(fun evArgs -> binButtonClicked evArgs binbtn)
octalbtn.Click.Add(fun evArgs -> octalButtonClicked evArgs octalbtn)

form.Controls.Add(binbtn)
form.Controls.Add(hexbtn)
form.Controls.Add(octalbtn)

[<STAThread>]
Application.Run(form)
