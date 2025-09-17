#define SPEAKER_PIN 11
#define POTENTIOMETER_PIN A1
#define BUTTON_PIN 8
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
  pinMode(SPEAKER_PIN,OUTPUT);
  pinMode(POTENTIOMETER_PIN,INPUT);
  pinMode(BUTTON_PIN,INPUT_PULLUP);
}
void loop() {
  if (digitalRead(8) == LOW) playMorseCode();
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
          if (map(analogRead(A1), 0, 1023, 0, part2.toInt()) != currentValue)
          {
            currentValue = map(analogRead(A1), 0, 1023, 0, part2.toInt());
            Serial.println(currentValue);
          }
          break;
       case 'B':
          Serial.println(analogRead(A1));
          break;
    }
  }
}
