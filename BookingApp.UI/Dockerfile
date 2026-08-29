#install dependencies
FROM node:24.20.0-alpine AS base
WORKDIR /app
COPY package.json package-lock.json ./
RUN --mount=type=cache,target=/root/.npm npm ci

#dev
FROM base AS dev
COPY . .
EXPOSE 5173
CMD ["npm", "run", "dev", "--", "--host"]

#compile static files
FROM base AS builder
COPY . .
RUN npm run build

#serve the app
FROM node:24.20.0-alpine AS prod
ENV NODE_ENV=production
WORKDIR /app
COPY --link --from=builder /app/dist ./dist
RUN --mount=type=cache,target=/root/.npm npm install serve@^14.2.4 --omit=dev
USER node
EXPOSE 3000
CMD ["npx", "serve", "-s", "dist", "-l", "3000"]