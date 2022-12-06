# Urban TimeTravel - SeatSync

<img src="https://user-images.githubusercontent.com/40402725/205653049-2c3112a4-d7de-4904-abec-c038f3ad95ea.png">

This plugin generically generates seats with characters and assigns a QR code to each seat. This QR code can be scanned using VR glasses and the character on the assigned seat is then activated. Once activated, the VR camera rotations are transferred to the character's head. When multiple VR glasses scan an assigned QR code, the users can see the other VR users sitting next to them and recognize which direction the character is currently facing based on the head rotation of the respective character.

An MQTT broker is required for use. The VR glasses communicate via this broker.

# Add package

Download the latest release package and import it into Unity.

# Folder structur

In the UTTSeatSync/Prefab/Manager are the two managers that are needed. MQTT connects to the MQTT broker and manages the communication between the glasses.
SeatGenerator allows the creation of the seats and the corresponding QR codes.

# Seat Generator

After the Seat Max Row and Seat Max Column have been defined in the Seat Generator, it is possible to create the seats and QR Codes via the button "Create Seats".
After that the VR Camera has to be added to the Seat Generator at PlayerCamera. This must also be marked as the Main Camera.

The generated QR codes are saved in the "QrCodes" folder.

When the QR code of a seat is scanned, the VR glasses are placed on this seat with its character.

# QR code generator

ZXing is used to generate and read the QR codes. More detailed info about it can be found here: https://github.com/micjahn/ZXing.Net

# MQTT

For MQTT, the IP of the MQTT broker must be specified.

When starting the application, the MQTT client first tries to establish a connection to the MQTT broker, only when this succeeds does the application work.

A more detailed explanation of the MQTT scripts can be found here: https://github.com/gpvigano/M2MqttUnity

# Character requirements

The character in the example scene can be replaced by any other character. It is only important that this character has a head, on which the script "Catch Head Rotation" must be placed.

# Examples

In the SampleScene is an example how to use it.

In the example scene the Pico SDK of the VR glasses "Pico G2 4K" was used, the Pvr_UnitySDK Prefab of the Pico glasses can be replaced by any other camera. 

Pico SDK Source: https://github.com/picoxr/support
<p align="center">
  <img width="400" height="400" src="https://user-images.githubusercontent.com/40402725/205893279-dea7d3ec-f44a-4afa-83a6-98b97b88d218.png">
</p>
