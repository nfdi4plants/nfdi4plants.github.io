# Guide for Mac & Linux
## Local development
Install prerequisites
`gem install bundler jekyll`

Install bundle
`bundle`

Run Jekyll
`bundle exec jekyll serve --livereload`

## Deployment
`bundle exec jekyll build`
Ready-to-deploy site found in `_site` folder.

# Guide using Docker (Windows)
1. [Install docker](https://docs.docker.com/docker-for-windows/install/)
2. Get Jekyll Docker: 
`docker pull jekyll/jekyll`

## Build your site
* Go the location of your page
`cd /PathToYourSite/`
* Run Jekyll build
`docker-compose up "build"`
* Ready-to-deploy site found in `_site` folder.

## Test your site at localhost
* Go the location of your page
`cd /PathToYourSite/`
* Run Jekyll site
`docker-compose up "site"`
* Find your page under http://localhost:4000/
