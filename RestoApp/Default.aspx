<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestoApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .home-hero {
            align-items: center;
            background:
                linear-gradient(115deg, rgba(18, 27, 33, 0.78), rgba(49, 85, 79, 0.52)),
                url("https://images.unsplash.com/photo-1551218808-94e220e084d2?auto=format&fit=crop&w=1920&q=80") center/cover no-repeat !important;
            display: flex;
            min-height: min(76vh, 640px) !important;
            padding: clamp(4rem, 7vw, 6rem) 0 !important;
            text-align: left;
        }

        .home-hero .container {
            max-width: 1180px;
        }

        .home-kicker {
            color: #f3cf8c;
            font-size: 0.85rem;
            font-weight: 800;
            letter-spacing: 0.18em;
            text-transform: uppercase;
        }

        .home-hero h1 {
            max-width: 760px;
        }

        .home-hero p {
            margin-left: 0;
            max-width: 600px;
        }

        .home-hero-actions {
            display: flex;
            flex-wrap: wrap;
            gap: 0.75rem;
            margin-top: 1.5rem;
        }

        .home-feature-strip {
            margin-top: -54px;
            position: relative;
            z-index: 5;
        }

        .home-stat {
            align-items: center;
            display: flex;
            gap: 1rem;
        }

        .home-stat i {
            align-items: center;
            background: #fff3d6;
            border: 1px solid rgba(200, 155, 72, 0.35);
            border-radius: 50%;
            color: #9f543d;
            display: inline-flex;
            flex: 0 0 54px;
            font-size: 1.3rem;
            height: 54px;
            justify-content: center;
            width: 54px;
        }

        .home-section {
            padding: clamp(3rem, 6vw, 5rem) 0;
        }

        .section-eyebrow {
            color: var(--ok-primary);
            font-size: 0.78rem;
            font-weight: 800;
            letter-spacing: 0.16em;
            text-transform: uppercase;
        }

        .event-card img,
        .dish-card img {
            aspect-ratio: 4 / 3;
            object-fit: cover;
            width: 100%;
        }

        .event-date {
            background: #26343f;
            border-radius: 8px;
            color: #ffffff;
            display: inline-flex;
            flex-direction: column;
            font-weight: 800;
            min-width: 64px;
            padding: 0.7rem 0.8rem;
            text-align: center;
        }

        .event-date span:first-child {
            color: #f3cf8c;
            font-size: 0.75rem;
            letter-spacing: 0.12em;
            text-transform: uppercase;
        }

        .event-date span:last-child {
            font-size: 1.55rem;
            line-height: 1;
        }

        .chef-panel {
            background:
                linear-gradient(115deg, rgba(38, 52, 63, 0.88), rgba(49, 85, 79, 0.74)),
                url("https://images.unsplash.com/photo-1559339352-11d035aa65de?auto=format&fit=crop&w=1600&q=80") center/cover no-repeat;
            border-radius: 8px;
            color: #ffffff;
            overflow: hidden;
            padding: clamp(2rem, 5vw, 4rem);
            position: relative;
        }

        .chef-panel p {
            color: rgba(255, 255, 255, 0.82);
            max-width: 620px;
        }

        .home-pulse {
            animation: homePulse 2.8s ease-in-out infinite;
        }

        @keyframes homePulse {
            0%, 100% {
                box-shadow: 0 0 0 0 rgba(200, 155, 72, 0.34);
            }
            50% {
                box-shadow: 0 0 0 12px rgba(200, 155, 72, 0);
            }
        }

        @media (max-width: 767.98px) {
            .home-hero {
                min-height: auto !important;
                text-align: center;
            }

            .home-hero p {
                margin-inline: auto;
            }

            .home-hero-actions {
                justify-content: center;
            }

            .home-feature-strip {
                margin-top: -30px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section class="hero-section home-hero">
        <div class="container">
            <div class="home-kicker mb-3">Open kitchen, live evenings, warm tables</div>
            <h1>RestoApp brings dinner nights, chef events, and easy table booking together.</h1>
            <p class="lead">Reserve a table, browse the menu, and catch seasonal restaurant events before the best seats disappear.</p>
            <div class="home-hero-actions">
                <a href="~/ClientsView/Tables/Tables.aspx" runat="server" class="btn btn-primary btn-lg home-pulse">
                    <i class="fa-solid fa-calendar-check me-2"></i>Book a Table
                </a>
                <a href="~/ClientsView/Menu/Menu.aspx" runat="server" class="btn btn-outline-light btn-lg">
                    <i class="fa-solid fa-utensils me-2"></i>View Menu
                </a>
            </div>
        </div>
    </section>

    <div class="container home-feature-strip">
        <div class="card border-0">
            <div class="card-body">
                <div class="row g-4">
                    <div class="col-md-4">
                        <div class="home-stat">
                            <i class="fa-solid fa-fire-flame-curved"></i>
                            <div>
                                <h5 class="fw-bold mb-1">Fresh Daily</h5>
                                <p class="text-muted mb-0">Seasonal prep from lunch to late service.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="home-stat">
                            <i class="fa-solid fa-martini-glass-citrus"></i>
                            <div>
                                <h5 class="fw-bold mb-1">Curated Nights</h5>
                                <p class="text-muted mb-0">Music, tasting menus, and private dining.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="home-stat">
                            <i class="fa-solid fa-chair"></i>
                            <div>
                                <h5 class="fw-bold mb-1">Fast Booking</h5>
                                <p class="text-muted mb-0">Pick your table and send a request in minutes.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section class="home-section">
        <div class="container">
            <div class="d-flex justify-content-between align-items-end flex-wrap gap-3 mb-4">
                <div>
                    <div class="section-eyebrow mb-2">Upcoming Events</div>
                    <h2 class="fw-bold mb-0">Static nights ready for the homepage</h2>
                </div>
                <a href="~/ClientsView/Tables/Tables.aspx" runat="server" class="btn btn-outline-primary">
                    Reserve for an Event
                </a>
            </div>

            <div class="row g-4">
                <div class="col-lg-4">
                    <div class="card event-card h-100" data-aos="fade-up">
                        <img src="https://images.unsplash.com/photo-1514933651103-005eec06c04b?auto=format&fit=crop&w=900&q=80" alt="Jazz dinner night" />
                        <div class="card-body">
                            <div class="d-flex gap-3 mb-3">
                                <div class="event-date"><span>May</span><span>24</span></div>
                                <div>
                                    <h5 class="fw-bold mb-1">Jazz Dinner Night</h5>
                                    <p class="text-muted mb-0">A relaxed dinner set with live sax, seafood specials, and candlelit tables.</p>
                                </div>
                            </div>
                            <span class="badge bg-dark">7:30 PM</span>
                            <span class="badge bg-warning text-dark ms-1">Live Music</span>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="card event-card h-100" data-aos="fade-up" data-aos-delay="90">
                        <img src="https://images.unsplash.com/photo-1555244162-803834f70033?auto=format&fit=crop&w=900&q=80" alt="Chef tasting table" />
                        <div class="card-body">
                            <div class="d-flex gap-3 mb-3">
                                <div class="event-date"><span>May</span><span>29</span></div>
                                <div>
                                    <h5 class="fw-bold mb-1">Chef's Tasting Table</h5>
                                    <p class="text-muted mb-0">Five courses built around local produce, grilled mains, and plated desserts.</p>
                                </div>
                            </div>
                            <span class="badge bg-dark">8:00 PM</span>
                            <span class="badge bg-success ms-1">Limited Seats</span>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="card event-card h-100" data-aos="fade-up" data-aos-delay="180">
                        <img src="https://images.unsplash.com/photo-1544148103-0773bf10d330?auto=format&fit=crop&w=900&q=80" alt="Weekend brunch" />
                        <div class="card-body">
                            <div class="d-flex gap-3 mb-3">
                                <div class="event-date"><span>Jun</span><span>02</span></div>
                                <div>
                                    <h5 class="fw-bold mb-1">Weekend Brunch Social</h5>
                                    <p class="text-muted mb-0">Bright plates, fresh juice, pastries, and shareable tables for friends.</p>
                                </div>
                            </div>
                            <span class="badge bg-dark">11:00 AM</span>
                            <span class="badge bg-info text-dark ms-1">Family Friendly</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="home-section pt-0">
        <div class="container">
            <div class="chef-panel" data-aos="zoom-in">
                <div class="section-eyebrow mb-2 text-warning">Chef Highlight</div>
                <h2 class="fw-bold mb-3">Seasonal plates with a warm dining-room rhythm.</h2>
                <p class="lead mb-4">The kitchen leans on grilled seafood, crisp starters, bright salads, and rich desserts so every booking has a little ceremony without feeling too formal.</p>
                <a href="~/ClientsView/Menu/Menu.aspx" runat="server" class="btn btn-warning btn-lg">
                    Explore Signature Plates
                </a>
            </div>
        </div>
    </section>

    <section class="home-section pt-0">
        <div class="container">
            <div class="text-center mb-4">
                <div class="section-eyebrow mb-2">Popular Picks</div>
                <h2 class="fw-bold">A few reasons to stay for dessert</h2>
            </div>

            <div class="row g-4">
                <div class="col-md-4">
                    <div class="card dish-card h-100" data-aos="fade-up">
                        <img src="https://images.unsplash.com/photo-1467003909585-2f8a72700288?auto=format&fit=crop&w=900&q=80" alt="Grilled salmon plate" />
                        <div class="card-body">
                            <h5 class="fw-bold">Grilled Salmon</h5>
                            <p class="text-muted mb-0">Clean heat, lemon, herbs, and a polished dinner-service favorite.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card dish-card h-100" data-aos="fade-up" data-aos-delay="90">
                        <img src="https://images.unsplash.com/photo-1600891964092-4316c288032e?auto=format&fit=crop&w=900&q=80" alt="Ribeye steak" />
                        <div class="card-body">
                            <h5 class="fw-bold">Ribeye Steak</h5>
                            <p class="text-muted mb-0">A rich main with rosemary butter and enough drama for a night out.</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card dish-card h-100" data-aos="fade-up" data-aos-delay="180">
                        <img src="https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?auto=format&fit=crop&w=900&q=80" alt="Tiramisu dessert" />
                        <div class="card-body">
                            <h5 class="fw-bold">Classic Tiramisu</h5>
                            <p class="text-muted mb-0">Coffee, cream, cocoa, and the kind of ending people remember.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
