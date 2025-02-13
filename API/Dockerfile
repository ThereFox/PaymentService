FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS dist_base

RUN apk update
RUN apk add git
RUN apk add neofetch
RUN apk add zsh
RUN apk add neovim
RUN sh -c "$(wget https://raw.githubusercontent.com/ohmyzsh/ohmyzsh/master/tools/install.sh -O -)"
RUN git clone https://github.com/zsh-users/zsh-syntax-highlighting.git $ZSH_CUSTOM/plugins/zsh-syntax-highlighting
RUN git clone --depth=1 https://github.com/romkatv/powerlevel10k.git $(ZSH_CUSTOM:‑$HOME/.oh‑my‑zsh/custom}/themes/powerlevel10k) 

RUN echo \
    "plugins=(git zsh-syntax-highlighting) \
    ZSH_THEME="powerlevel10k/powerlevel10k" \
    POWERLEVEL10K_MODE="nerdfont-complete" \
    " >> ~/.zshrc

RUN zsh

ARG UID=10001

RUN adduser \
    --disabled-password \
    --gecos "" \
    #--home "/nonexistent" \
    #--shell "/sbin/nologin" \
    #--no-create-home \
    --uid "${UID}" \
    appuser

RUN cp ~/.zshrc /home/appuser/.zshrc
RUN cp -r ~/.oh-my-zsh /home/appuser/

USER appuser

WORKDIR app_bins

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore ./API/API.csproj
RUN dotnet build --output ./../build ./API/API.csproj
RUN dotnet publish --self-contained --output ./../dist ./API/API.csproj

FROM dist_base as dist

COPY --from=builder /dist .
ENTRYPOINT neofetch && /app_bins/API