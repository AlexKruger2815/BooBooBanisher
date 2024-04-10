# BooBooBanisher

Welcome to BooBooBanisher! 🎉

BooBooBanisher aims to sprinkle some motivation into your coding journey by displaying uplifting messages when you're knee-deep in bugs and errors.

## How It Works

BooBooBanisher monitors your coding sessions and when it senses frustration levels rising (read: when you've made a lot of mistakes), it pops up with motivational messages to cheer you on!

## Features

- Instant Motivation: Get a boost of encouragement precisely when you need it.
- Customizable Messages: Tailor motivational messages to fit your style.
- Lightweight: Doesn't add any extra weight to your workflow.

## Getting Started

To get started with BooBooBanisher, simply clone this repository and follow the installation instructions in the `README.md`.

```bash
git clone https://github.com/yourusername/BooBooBanisher.git
cd BooBooBanisher
```

## Infrastructure Setup
Prerequisites:
* Terraform v1.5.7
* Terragrunt v0.53.2
1. Navigate to the ./infrastructure/directory
2. Set these environment variables on your system for use when creating the DB:
    * `TF_VAR_password`
    * `TF_VAR_username`
4. Run this command to build the infrastructure:
   ``` sh
   terragrunt run-all apply
   ```

## The Banishers
- Alex Kruger
- Dané De Klerk
- Joseph Kimathi
- Liam Quick
- Shane Theron

## Links
For more detailed documentation and tips, check out our [Confluence page](https://bbd-dane.atlassian.net/jira/software/projects/CLUB/boards/5/backlog).

Stay up-to-date with our project progress and tasks by visiting our [Jira board](https://bbd-dane.atlassian.net/jira/software/projects/CLUB/boards/5).