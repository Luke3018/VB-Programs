int sonar_trigger_pin = D0;
int sonar_echo_pin = D1;
int x;
int SersorPin = D7;
int SensorValue = LOW;

unsigned int measure_distance()
{
  digitalWrite(sonar_trigger_pin, HIGH);
  delayMicroseconds(10);
  digitalWrite(sonar_trigger_pin, LOW);

  unsigned long pulse_length = pulseIn(sonar_echo_pin, HIGH);
  delay(50);
  return( (unsigned int) (pulse_length / 148) );
}

void setup() {
  pinMode(D2, OUTPUT);
  pinMode(D3, OUTPUT);
  pinMode(D6, OUTPUT);
  pinMode(D5, OUTPUT);
  pinMode(D4, OUTPUT);
  pinMode(D7, OUTPUT);
  pinMode(sonar_trigger_pin, OUTPUT);
  pinMode(sonar_echo_pin, INPUT);
  Serial.begin(9600);
  
}

void loop() {

 /*unsigned int current_distance = measure_distance();
 Serial.println(current_distance);
 delay(125);
 */

 Sensor();
 
 if (Serial.available())
 {
  int x = measure_distance();
  if (x >= 7 && x <= 1023)
  x = x * 50;
  else if (x <= 6)
  x = 1023;    
   forward(x);
  /*
    delay(1000);
    backwards(x);
    delay(1000);
    left(x);
    delay(1000);
    right(x);
    delay(1000);

    */

  if (SensorValue <= 3000)
    {
      analogWrite(D2, 0);
      analogWrite(D3, 0);
      analogWrite(D5, 0);
      analogWrite(D6, 0);
        
     }
  
  //Serial.println(x);

}
}


void forward(int x) {
  analogWrite(D2, x);
  analogWrite(D3, LOW);
  analogWrite(D5, x);
  analogWrite(D6, LOW);

}

void backwards(int x) {
  analogWrite(D2, LOW);
  analogWrite(D3, x);
  analogWrite(D5, LOW);
  analogWrite(D6, x);
}

void left(int x){
  analogWrite(D2, x);
  analogWrite(D3, LOW);
  analogWrite(D5, LOW);
  analogWrite(D6, x);
}

void right(int x){
  analogWrite(D2, LOW);
  analogWrite(D3, x);
  analogWrite(D5, x);
  analogWrite(D6, LOW);
}


void Sensor(){

  digitalWrite(D7, HIGH);
  delayMicroseconds(10);
  pinMode(D7, INPUT);
  long time = micros();
  while (digitalRead(SersorPin) == HIGH && micros() - time < 3000);
  int diff = micros() - time;
  SensorValue = diff;
  if(Serial.available() > 0);
  {
    Serial.println(SensorValue);
  }
  delay(500);
}
               
