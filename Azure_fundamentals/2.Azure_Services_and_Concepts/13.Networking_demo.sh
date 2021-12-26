1. Vnet 
2. Network Security Group(NSG)
3. Application Gateway
4. Two VMs along with the disks required by the VM 
5. And network interfaces that associate VMs with the VNet.


Vnet  CIDR: 172.16.0.0/16

Subnets:
------------>
1. VM Subnet --> 172.16.0.0/24
2. AppGateway Subnet --> 172.16.2.0/26
3. Management Subnet --> 172.16.3.0/24 

The Vnet has DNS Servers tab.
We can use the default Azure-provided DNS server and this will give access to the VMs in the network as well as enabling those VMs to access the internet.

If you are setting up a set of VMs where one of them is intended to be a domain controller, you want to use custom option then we can use the 
IP address of the domain controller VM.

