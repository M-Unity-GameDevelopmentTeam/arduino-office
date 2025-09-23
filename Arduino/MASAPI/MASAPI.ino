#include <MFRC522.h>
#define SPEAKER_PIN 9
#define POTENTIOMETER_PIN A1
#define RFID_SS_PIN 5
int ledPin1 = 2;
int ledPin2 = 3;
int ledPin3 = 4;
int leds[] = {ledPin1, ledPin2, ledPin3};
int buttonPin1 = 8;
int buttonPin2 = 7;
int buttonPin3 = 6;
int buttons[] = {buttonPin1, buttonPin2, buttonPin3};
MFRC522 rfid(RFID_SS_PIN,10);
int separator;
String part1;
String part2;
String data;
int currentValue;
String currentMorseCode = "..--.";
void playMorseCode() {
  for (int i = 0; i < currentMorseCode.length(); i++) {
    switch (currentMorseCode[i]) {
      case '.':
        tone(SPEAKER_PIN,200,200);
        delay(400);
        break;
      case ' ':
        delay(400);
        break;
      case '-':
        tone(SPEAKER_PIN,200,400);
        delay(600);
        break;
    }
  }
}
void setup() {
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();
  pinMode(SPEAKER_PIN, OUTPUT);
  pinMode(POTENTIOMETER_PIN, INPUT);
  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);
  pinMode(buttonPin1, INPUT_PULLUP);
  pinMode(buttonPin2, INPUT_PULLUP);
  pinMode(buttonPin3, INPUT_PULLUP);
}
void loop() {
  if (digitalRead(buttonPin1) == LOW) playMorseCode();
  if (Serial.available() > 0) {
    data = Serial.readStringUntil('\n');
    data.trim();
    separator = data.indexOf(':');
    part1 = data.substring(0, separator);
    part2 = data.substring(separator + 1);
    switch (part1[0]) {
      case 'M':
          currentMorseCode = part2;
          playMorseCode();
          break;
       case 'P':
          if (map(analogRead(POTENTIOMETER_PIN), 0, 1023, 0, part2.toInt()) != currentValue)
          {
            currentValue = map(analogRead(POTENTIOMETER_PIN), 0, 1023, 0, part2.toInt());
            Serial.println(currentValue);
          }
          break;
       case 'D':
          Serial.println(analogRead(POTENTIOMETER_PIN));
          break;
       case 'K':
          Serial.println(rfid.PICC_IsNewCardPresent());
          break;
       case 'B':
          Serial.println(part2 + ":" + digitalRead(buttons[part2.toInt()]));
          break;
       case 'H':
          digitalWrite(leds[part2.toInt()], HIGH);
          break;
       case 'L':
          digitalWrite(leds[part2.toInt()], LOW);
          break;
          
    }
  }
}
