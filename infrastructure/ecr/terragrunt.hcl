terraform {
  source = "tfr://registry.terraform.io/terraform-aws-modules/ecr/aws//?version=2.2.0"
}

include "root" {
  path = find_in_parent_folders()
}

inputs = {
  repository_name = "booboobanisher"
  
  repository_lifecycle_policy = jsonencode({
    rules = [
      {
        rulePriority = 1,
        description  = "Keep last 30 images",
        selection = {
          tagStatus     = "tagged",
          tagPrefixList = ["v"],
          countType     = "imageCountMoreThan",
          countNumber   = 30
        },
        action = {
          type = "expire"
        }
      }
    ]
  })
}