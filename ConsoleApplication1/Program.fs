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
label.Location <- new Point(50, 25)
label.Text <- "Select a learning module below to get started"

form.Controls.Add(label)

let introbtn = new Button(Text = "Intro To Bases", Width = 350, Height = 75)
introbtn.Location <- new Point(175, 75)
let binbtn = new Button(Text = "Binary Lesson", Width = 350, Height = 75)
binbtn.Location <- new Point(175, 175)
let hexbtn = new Button(Text = "Hex Lesson", Width = 350, Height = 75)
hexbtn.Location <- new Point(175, 275)
let octalbtn = new Button(Text = "Octal Lesson", Width = 350, Height = 75)
octalbtn.Location <- new Point(175, 375)
let otherbtn = new Button(Text = "Other Bases", Width = 350, Height = 75)
otherbtn.Location <- new Point(175, 475)

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

let introButtonClicked evArgs sender =
    // make new form and populate
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 650, Height = 650)
    form.Visible <- true
    form.Text <- "Intro to Number Bases"

    let introlabel = new Label(Width = 550, Height = 400, Location = new Point(50,25))

    introlabel.Text <- "Most people are used to what are called decimal numbers or “base 10” numbers. They’re called base 10 because each their digits correspond to a different power of ten.
\nFor example, let’s look at the number 35,709. This can be broken up into powers of ten like so:
\n     35,709 = 3*(10,000) + 5*(1,000) + 7*(100) + 0*(10) + 9*(1)
                 = 3*(10^4) + 5*(10^3) + 7*(10^2) + 0*(10^1) + 9*(10^0)
\nFor different applications in STEM, it can be helpful to represent numbers in different bases than base 10. 
\nFor example, computers use binary to store data because of their hardware design. Each digit of a binary number has two possible values—one or zero, on or off.
\nThat being said, it would be ridiculous to try to use binary are our standard representation for numbers. Imagine writing a check or calculating a tip using ones and zeros! While these calculations are still possible with binary, and computers calculate things like this in binary all the time, it seems that people are most comfortable using decimal numbers.
\nThere has been debate whether we should adopt a base 12 system for everyday use, but the next few tutorials will be focused on teaching you the three most common base conversions in STEM applications—binary, hex, and octal.

"


    form.Controls.Add(introlabel)

    ()

let hexButtonClicked evArgs sender =
    // make new form and populate
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 650, Height = 650)
    form.Visible <- true
    form.Text <- "Hexadecimal lesson"

    let hexlabel = new Label(Width = 550, Height = 400, Location = new Point(50,25))

    hexlabel.Text <- "Hexadecimal – also known as hex or base 16 – is a system we can use to write and share numerical values. In that way it’s no different than the most famous of numeral systems (the one we use every day): decimal. Decimal is a base 10 number system (perfect for beings with 10 fingers), and it uses a collection of 10 unique digits, which can be combined to positionally represent numbers.
     \nHex, like decimal, combines a set of digits to create large numbers. It just so happens that hex uses a set of 16 unique digits. \n\nHex uses the standard 0-9, but it also incorporates six digits you wouldn’t usually expect to see creating numbers: A, B, C, D, E, and F.Hexadecimal is a base-16 number system. That means there are 16 possible digits used to represent numbers. 10 of the numerical values you’re probably used to seeing in decimal numbers: 0, 1, 2, 3, 4, 5, 6, 7, 8, and 9; those values still represent the same value you’re used to. The remaining six digits are represented by A, B, C, D, E, and F, which map out to values of 10, 11, 12, 13, 14, and 15.
     \n\n Try converting decimal numbers in the box below to see what the hex will be!"

    form.Controls.Add(hexlabel)

    let decBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,455))
    let decLabel = new Label(Width = 100, Height = 50, Location = new Point(300,455))
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
    | [_] -> [0.0]
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

    let subDecBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 440), Text = "Dec -> Hex")
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

    let decBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,455))
    let decLabel = new Label(Width = 100, Height = 50, Location = new Point(300,455))
    decLabel.Text <- "0"

    let octalBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,515))
    let octalLabel = new Label(Width = 100, Height = 50, Location = new Point(300,515))
    octalLabel.Text <- "0"

    let rec octalconvert s = 
        match s with
    | 0 -> ""
    | _ -> octalconvert(s / 8) ^ int2String (s % 8)

    let rec makedecarr list =
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
    | [_] -> [0.0]
    | head::tail -> (makedecarr [head])@(makedecarr tail)
    
    let rec decconvert list n =
        match list with
    | [] -> 0.0
    | [x] -> ((x |> float)*(8.0**n))
    | head::tail -> (decconvert [head] n) + (decconvert tail (n-1.0))

    let subOctalBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 500), Text = "Dec -> Octal")
    let subOctalButtonClicked evArgs sender =
        let convert = octalBox.Text |> int  //handle exceptions here
        octalLabel.Text <- octalconvert convert
    ()

    let subDecBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 440), Text = "Octal -> Dec")
    let subDecButtonClicked evArgs sender =
        let convert = String.explode decBox.Text
        let decarr = makedecarr convert
        let decresult = decconvert decarr (decarr.Length-1 |> float)
        decLabel.Text <- decresult |> string //output result
        ()
    ()

    subOctalBtn.Click.Add(fun evArgs -> subOctalButtonClicked evArgs octalbtn)
    subDecBtn.Click.Add(fun evArgs -> subDecButtonClicked evArgs octalbtn)

    form.Controls.Add(octalBox)
    form.Controls.Add(octalLabel)
    form.Controls.Add(subOctalBtn)
    form.Controls.Add(decBox)
    form.Controls.Add(decLabel)
    form.Controls.Add(subDecBtn)

()

let binButtonClicked evArgs sender =
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 650, Height = 650)
    form.Visible <- true
    form.Text <- "Bin lesson"

    let binlabel = new Label(Width = 550, Height = 375, Location = new Point(50,25))

    binlabel.Text <- "Binary numbers are the most common alternative base to decimal. Ever heard that computers \"speak\" in ones and zeros? Well, they kind of do!
    \nSince digital logic relies on circuitry, it is helpful to represent numbers where each decimal only has two states: on or off--1 or 0. In fact, all digital computation can be reduced to zeros and ones.
    \nComputer hardware is designed to perform differently depeding on what parts of the hardware are recieving current and which aren't.
    \nIf you want to learn more about why binary is the basis for computer logic, check out Allan Turing and his \"Turing Machine\" or the digital logic behind TVs, microwaves, and digital clocks.
    \nNow let's talk about how to make sense of binary numbers. As in the introduction, we have to think about the base of binary numbers: 2. Here's an example to get started:
    \n 
    "

    form.Controls.Add(binlabel)

    let decBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,455))
    let decLabel = new Label(Width = 100, Height = 50, Location = new Point(300,455))
    decLabel.Text <- "0"

    let binBox = new TextBox(Width = 100, Height = 50, Location = new Point(175,515))
    let binLabel = new Label(Width = 100, Height = 50, Location = new Point(300,515))
    binLabel.Text <- "0"

    let rec binconvert s = 
        match s with
    | 0 -> ""
    | _ -> binconvert(s / 2) ^ int2String (s % 2)

    let rec makedecarr list =
        match list with
    | [] -> []
    | ['0'] -> [0.0]
    | ['1'] -> [1.0]
    | [_] -> [0.0]
    | head::tail -> (makedecarr [head])@(makedecarr tail)
    
    let rec decconvert list n =
        match list with
    | [] -> 0.0
    | [x] -> ((x |> float)*(2.0**n))
    | head::tail -> (decconvert [head] n) + (decconvert tail (n-1.0))

    let subBinBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 500), Text = "Dec -> Binary")
    let subBinButtonClicked evArgs sender =
        let convert = binBox.Text |> int //handle exceptions here
        binLabel.Text <- binconvert convert
    ()

    let subDecBtn = new Button(Width = 100, Height = 50, Location = new Point(400, 440), Text = "Bin -> Dec")
    let subDecButtonClicked evArgs sender =
        let convert = String.explode decBox.Text
        let decarr = makedecarr convert
        let decresult = decconvert decarr (decarr.Length-1 |> float)
        decLabel.Text <- decresult |> string //output result
        ()
    ()

    subBinBtn.Click.Add(fun evArgs -> subBinButtonClicked evArgs binbtn)
    subDecBtn.Click.Add(fun evArgs -> subDecButtonClicked evArgs binbtn)

    form.Controls.Add(binBox)
    form.Controls.Add(binLabel)
    form.Controls.Add(subBinBtn)
    form.Controls.Add(decBox)
    form.Controls.Add(decLabel)
    form.Controls.Add(subDecBtn)

()

let otherButtonClicked evArgs sender =
    // make new form and populate
    let formcontrol = [for x in 0..4 -> x]
    let current = formcontrol.[0]
    let form = new Form(Width = 750, Height = 750)
    form.Visible <- true
    form.Text <- "Intro to Number Bases"

    let otherlabel = new Label(Width = 700, Height = 700, Location = new Point(25,25))

    otherlabel.Text <- "While it’s unlikely you’ll need to convert to anything other than binary, hex, or octal, here’s a quick look at how you can convert from any base to any other base. Let’s start with base ten to an unfamiliar base. This should take the same basic form as the conversions we saw in other tutorials!\n
        1,234 decimal to base 3\n
        Quotient     Result     Remainder
        1234/3        411        1
        411/3          137        0
        137/3          45         2
        45/3            15         0
        15/3            5           0
        5/3             1            2
        1/3             0            1
\nReading from the bottom up, the remainders give us that
        1,234 in decimal = 1200201 in base 3
\nPretty straight forward. Let's try converting from an unfamiliar base to base 10.\n
        1200201 base 3 to decimal\n
        1200201 = 1*3^6 + 2*3^5 + 0*3^4 + 0*3^3 + 2*3^2 + 0*3^1 + 1*3^0
                = 1*729 + 2*243 + 0 + 0 + 2*9 + 0 + 1*1
                = 729 + 486 + 18 + 1
                = 1234
\nNow let's try converting between two non-decimal numbers using a combination of these two base conversion algorithms.\n
        1200201 base 3 to base 5
\nWe start with the same process as above by converting our base 3 number to base 10.\n
        1200201 = 1*3^6 + 2*3^5 + 0*3^4 + 0*3^3 + 2*3^2 + 0*3^1 + 1*3^0
                = 1*729 + 2*243 + 0 + 0 + 2*9 + 0 + 1*1
                = 729 + 486 + 18 + 1
                = 1234
\nThen we compute the base 5 version of 1,234.\n
        Quotient   Result    Remainder
        1234/5      246         4
        246/5        49          1
        49/5          9            4
        9/5           1            4
        1/5           0            1\n
\nSo 1,234 in decimal = 44414 in base 5.
So 1200201 base 3 = 44414 base 5.

To find other number base conversions, just follow this algorithm!
"


    form.Controls.Add(otherlabel)

    ()

introbtn.Click.Add(fun evArgs -> introButtonClicked evArgs introbtn)
hexbtn.Click.Add(fun evArgs -> hexButtonClicked evArgs hexbtn)
binbtn.Click.Add(fun evArgs -> binButtonClicked evArgs binbtn)
octalbtn.Click.Add(fun evArgs -> octalButtonClicked evArgs octalbtn)
otherbtn.Click.Add(fun evArgs -> otherButtonClicked evArgs otherbtn)

form.Controls.Add(introbtn)
form.Controls.Add(binbtn)
form.Controls.Add(hexbtn)
form.Controls.Add(octalbtn)
form.Controls.Add(otherbtn)

[<STAThread>]
Application.Run(form)
