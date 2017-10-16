# Expertise

This repo contains source code and content and generated content for expertise website.

# Fork and Setup

The website is generated with DocFX with plugins. More complete procedure:

1. Install DocFX, VS 2017.
2. Open plugin/exprtise-extras.sln
3. Compile under Release.
4. Copy images under docs/images/ to images/ 
5. `docfx --serve`

# Contents

| File/Folder | Desciption |
|---|---|
| docs/ | Generated website |
| images/ | Image files |
| plugin/ | Plugin source code |
| template/ | Custom template files |
| docfx.json | Global DocFX config |
| index.md | Website Content |

# Notes

+ `index.md` is the only website content.
+ images is git ignored because it's a duplicate content with generated docs.
+ generated plugins is also git ignored and its release compile will be placed under `templates/plugins/`

# License
Content: CC-BY-SA-4.0
Plugin & Theme: MIT 