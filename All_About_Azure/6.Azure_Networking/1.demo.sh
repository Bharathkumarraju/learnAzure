Networking is the foundation of cloud security.

Brute force attacks on port 3389.
No line of Defence in front of the VM.

Vnets
Subnets
Loadbalancer
Application Gateway

Resources in VNet can communicate with each other by default.
VNets are free 

VNet is scoped to a single Region.
VNet is scoped to single subscription.

VNets are Protected using NSG(using subnets)

Each VNet has its own address range.


CIDR:
------------->
00000000 = 0
11111111 = 255

109.186.149.240/24
24 bits allocated to address
8 bits allocated for range

109.186.149.240/24
109.186.149.000 - 109.186.149.255 --> 256 Addresses


109.186.149.240/16  --> 16 bits allocated to address, 16 bits allocated for range
109.186.000.000 - 109.186.255.255 --> 65,536 Addresses

109.186.149.240/20 --> 20 bits allocated to address, 12 bits allocated for range 
109.186.144.0 - 109.186.159.255  --> 4096 IPs.

Resources in a subnet can talk to resources in other subnets in the same VNet.

NSG is a gatekeeper for subnets
