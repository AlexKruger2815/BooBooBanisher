terraform {
  backend "s3" {
    bucket = "BooBooBanisherbucket"
    key = "BooBooBanisher/terraform.tfstate"  # Specify the path/key for your state file
    region = "eu-west-1"
  }
}