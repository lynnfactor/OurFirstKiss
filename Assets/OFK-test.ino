/* written by Aubrey Isaacman
 *  
 *  using the button example for arduino uno
 *    http://www.arduino.cc/en/Tutorial/Button
*/

// constants won't change. They're used here to set pin numbers:
const int button1Pin = 1;     // the number of the pushbutton pin
const int button2Pin = 2;
const int button3Pin = 3;
const int button4Pin = 4;
const int button5Pin = 5;

const int led1Pin =  8;      // the number of the LED pin
const int led2Pin = 9;
const int led3Pin = 10;
const int led4Pin = 11;
const int led5Pin = 12;

// variables will change:
int button1State = 0;         // variable for reading the pushbutton status
int button2State = 0;
int button3State = 0;
int button4State = 0;
int button5State = 0;

void setup() {
  // initialize the LED pin as an output:
  pinMode(led1Pin, OUTPUT);
  pinMode(led2Pin, OUTPUT);
  pinMode(led3Pin, OUTPUT);
  pinMode(led4Pin, OUTPUT);
  pinMode(led5Pin, OUTPUT);
  
  // initialize the pushbutton pin as an input:
  pinMode(button1Pin, INPUT);
  pinMode(button2Pin, INPUT);
  pinMode(button3Pin, INPUT);
  pinMode(button4Pin, INPUT);
  pinMode(button5Pin, INPUT);
}

void loop() {
  // read the state of the pushbutton value:
  button1State = digitalRead(button1Pin);
  button2State = digitalRead(button2Pin);
  button3State = digitalRead(button3Pin);
  button4State = digitalRead(button4Pin);
  button5State = digitalRead(button5Pin);
  
  // check if the pushbutton is pressed. If it is, the buttonState is HIGH:

  // far left button and LED
  if (button1State == HIGH) {
    // turn LED on:
    digitalWrite(led1Pin, HIGH);
  } else {
    // turn LED off:
    digitalWrite(led1Pin, LOW);
  }

  // just left button and LED
  if (button2State == HIGH) {
    // turn LED on:
    digitalWrite(led2Pin, HIGH);
    Serial.println("p1 move right");
  } else {
    // turn LED off:
    digitalWrite(led2Pin, LOW);
  }

  // middle button and LED
  if (button3State == HIGH) {
    // turn LED on:
    digitalWrite(led3Pin, HIGH);
    Serial.println("KISS!");
  } else {
    // turn LED off:
    digitalWrite(led3Pin, LOW);
  }

  // just right button and LED
  if (button4State == HIGH) {
    // turn LED on:
    digitalWrite(led4Pin, HIGH);
    Serial.println("p2 move left");
  } else {
    // turn LED off:
    digitalWrite(led4Pin, LOW);
  }

  // far right button and LED
  if (button5State == HIGH) {
    // turn LED on:
    digitalWrite(led5Pin, HIGH);
  } else {
    // turn LED off:
    digitalWrite(led5Pin, LOW);
  }
  
}
