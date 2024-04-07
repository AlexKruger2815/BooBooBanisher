terraform {
    source = "tfr://registry.terraform.io/terraform-aws-modules/vpc/aws//?version=5.5.2"
}

include "root" {
  path = find_in_parent_folders()
}

inputs = {

    name = "BooBooBanisher-VPC"
    cidr = "10.0.0.0/24"
    azs = ["eu-west-1a", "eu-west-1b"]


    public_subnets = ["10.0.0.0/26", "10.0.0.64/26"]
    public_subnet_suffix = "public"
    
    private_subnets = ["10.0.0.128/26", "10.0.0.192/26"]
    private_subnet_suffix = "private"
}