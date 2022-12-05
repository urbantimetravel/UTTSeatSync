# UTTSeatSync

![image](https://user-images.githubusercontent.com/40402725/205653049-2c3112a4-d7de-4904-abec-c038f3ad95ea.png)

This plugin generically generates seats with characters and assigns a QR code to each seat. This QR code can be scanned using VR glasses and the character on the assigned seat is then activated. Once activated, the VR camera rotations are transferred to the character's head. When multiple VR glasses scan an assigned QR code, the users can see the other VR users sitting next to them and recognize which direction the character is currently facing based on the head rotation of the respective character.

An MQTT broker is required for use. The VR glasses communicate via this broker.

# Add package

Add the package to your project via the Unity Package Manager using "Add package from git url...".

# Getting Started

In the Prefab/Manager are the two managers that are needed. MQTT connects to the MQTT broker and manages the communication between the glasses.
SeatGenerator allows the creation of the seats and the corresponding QR codes.

After the Seat Max Row and Seat Max Column have been defined in the Seat Generator, it is possible to create the seats and QR Codes via the button "Create Seats".
After that the VR Camera has to be added to the Seat Generator at PlayerCamera. This must also be marked as the Main Camera.

For MQTT, the IP of the MQTT broker must be specified.

When the QR code of a seat is scanned, the VR glasses are placed on this seat with its character.

# Examples

In the SampleScene is an example how to use it.

In the example scene the Pico SDK of the VR glasses "Pico G2 4K" was used, the Pvr_UnitySDK Prefab of the Pico glasses can be replaced by any other camera. 
