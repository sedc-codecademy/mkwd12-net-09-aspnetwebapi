class LoginRequest {
    constructor(username, password) {
        this.username = username
        this.password = password
    }
}

const PriorityEnum = {
    1: "Low",
    2: "Medium",
    3: "High"
}

const TagEnum = {
    1: "Work",
    2: "Health",
    3: "SocialLife"
}

export { LoginRequest, PriorityEnum, TagEnum }