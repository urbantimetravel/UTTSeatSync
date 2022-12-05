# UTTSeatSync

![image](https://user-images.githubusercontent.com/40402725/205653049-2c3112a4-d7de-4904-abec-c038f3ad95ea.png)

This plugin generically generates seats with characters and assigns a QR code to each seat. This QR code can be scanned using VR glasses and the character on the assigned seat is then activated. Once activated, the VR camera rotations are transferred to the character's head. When multiple VR glasses scan an assigned QR code, the users can see the other VR users sitting next to them and recognize which direction the character is currently facing based on the head rotation of the respective character.

An MQTT broker is required for use. The VR glasses communicate via this broker.
