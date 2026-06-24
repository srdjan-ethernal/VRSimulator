const translations = {
  sr: {
    metaTitle: "VR simulacije za industrijsku bezbednost",
    metaDescription:
      "Profesionalne VR simulacije za obuku iz protivpozarne zastite, upravljanja radioaktivnim materijalima i drugih industrijskih scenarija.",
    brand: { home: "Pocetak" },
    language: { label: "Izbor jezika" },
    nav: {
      label: "Glavna navigacija",
      open: "Otvori navigaciju",
      scenarios: "Scenariji",
      method: "Metodologija",
      contact: "Kontakt",
      cta: "Zakazite razgovor",
    },
    hero: {
      eyebrow: "VR obuke za visokorizicne procese",
      title: "VR simulacije za industrijsku bezbednost",
      copy: "Realisticne, kontrolisane i merljive obuke za timove koji rade u zahtevnim bezbednosnim okruzenjima.",
      actions: "Primarne akcije",
      primary: "Pogledajte scenarije",
      secondary: "Kontakt",
    },
    metrics: {
      label: "Pregled vrednosti",
      active: "aktivna scenarija",
      controlled: "kontrolisano okruzenje za obuku",
      learning: "imerzivno ucenje kroz praksu",
    },
    scenarios: {
      eyebrow: "Slucajevi upotrebe",
      title: "Scenariji spremni za prezentaciju i razvoj",
      copy: "Trenutni fokus je na protivpozarnoj zastiti i rukovanju radioaktivnim materijalima. Struktura je pripremljena za dodavanje novih oblasti.",
    },
    method: {
      eyebrow: "Metodologija",
      title: "Od scenarija do merljive obuke",
      copy: "Svaka simulacija se projektuje oko stvarnih procedura, kriticnih odluka i ponasanja koje organizacija zeli da uvezba pre rada u realnom okruzenju.",
    },
    steps: {
      analysis: {
        title: "Analiza procesa",
        copy: "Definisanje rizika, uloga, prostora, opreme i bezbednosnih protokola.",
      },
      build: {
        title: "Izrada simulacije",
        copy: "Modelovanje scenarija, interakcija, instrukcija i grananja odluka.",
      },
      evaluate: {
        title: "Evaluacija obuke",
        copy: "Pracenje ucinka, gresaka, vremena reakcije i spremnosti polaznika.",
      },
    },
    benefits: {
      eyebrow: "Prednosti",
      title: "Bezbedna praksa pre realnog rizika",
      risk: {
        title: "Manji operativni rizik",
        copy: "Polaznici mogu da vezbaju opasne situacije bez izlaganja ljudi, opreme ili prostora.",
      },
      standard: {
        title: "Standardizovana obuka",
        copy: "Isti scenario, kriterijumi i povratne informacije za svaku grupu i lokaciju.",
      },
      iterate: {
        title: "Brze iteracije",
        copy: "Novi scenariji, zadaci i greske mogu se dodavati kako se menjaju procedure.",
      },
    },
    contact: {
      eyebrow: "Sledeci korak",
      title: "Razgovarajmo o scenarijima koje treba simulirati",
      copy: "Posaljite kratak opis oblasti, ciljne grupe i procedura koje treba uvezbati. Na osnovu toga se definise obim prve demonstracije.",
    },
    footer: {
      copy: "Profesionalne VR simulacije za bezbednosne obuke.",
    },
  },
  en: {
    metaTitle: "VR simulations for industrial safety",
    metaDescription:
      "Professional VR simulations for fire protection training, radioactive material handling, and other industrial safety scenarios.",
    brand: { home: "Home" },
    language: { label: "Language selection" },
    nav: {
      label: "Main navigation",
      open: "Open navigation",
      scenarios: "Scenarios",
      method: "Methodology",
      contact: "Contact",
      cta: "Schedule a call",
    },
    hero: {
      eyebrow: "VR training for high-risk processes",
      title: "VR simulations for industrial safety",
      copy: "Realistic, controlled, and measurable training for teams operating in demanding safety environments.",
      actions: "Primary actions",
      primary: "View scenarios",
      secondary: "Contact",
    },
    metrics: {
      label: "Value overview",
      active: "active scenarios",
      controlled: "controlled training environment",
      learning: "immersive learning through practice",
    },
    scenarios: {
      eyebrow: "Use cases",
      title: "Scenarios ready for presentation and development",
      copy: "The current focus is fire protection and radioactive material handling. The structure is prepared for adding new domains later.",
    },
    method: {
      eyebrow: "Methodology",
      title: "From scenario to measurable training",
      copy: "Each simulation is designed around real procedures, critical decisions, and behaviors the organization wants to practice before work in a live environment.",
    },
    steps: {
      analysis: {
        title: "Process analysis",
        copy: "Defining risks, roles, spaces, equipment, and safety protocols.",
      },
      build: {
        title: "Simulation build",
        copy: "Modeling scenarios, interactions, instructions, and decision branches.",
      },
      evaluate: {
        title: "Training evaluation",
        copy: "Tracking performance, mistakes, response time, and trainee readiness.",
      },
    },
    benefits: {
      eyebrow: "Benefits",
      title: "Safe practice before real risk",
      risk: {
        title: "Lower operational risk",
        copy: "Trainees can practice hazardous situations without exposing people, equipment, or facilities.",
      },
      standard: {
        title: "Standardized training",
        copy: "The same scenario, criteria, and feedback across every group and location.",
      },
      iterate: {
        title: "Faster iteration",
        copy: "New scenarios, tasks, and mistakes can be added as procedures evolve.",
      },
    },
    contact: {
      eyebrow: "Next step",
      title: "Let us discuss the scenarios you need to simulate",
      copy: "Send a short description of the domain, target group, and procedures that need to be practiced. From there, we can define the scope of the first demonstration.",
    },
    footer: {
      copy: "Professional VR simulations for safety training.",
    },
  },
};

const scenarios = [
  {
    image: "assets/fire-protection-vr.png",
    variant: "is-warning",
    content: {
      sr: {
        title: "Protivpozarna zastita",
        label: "Bezbednosna obuka",
        alt: "VR obuka za protivpozarnu zastitu",
        description:
          "Simulacija uvodi polaznike u prepoznavanje rizika, evakuaciju, izbor opreme i pravilno reagovanje u kontrolisanim uslovima.",
        points: [
          "Postupanje pri pojavi dima i plamena",
          "Izbor aparata i bezbedna udaljenost",
          "Evakuacione odluke pod pritiskom",
        ],
      },
      en: {
        title: "Fire protection",
        label: "Safety training",
        alt: "VR training for fire protection",
        description:
          "The simulation introduces trainees to risk recognition, evacuation, equipment selection, and correct response in controlled conditions.",
        points: [
          "Response to smoke and flame conditions",
          "Extinguisher selection and safe distance",
          "Evacuation decisions under pressure",
        ],
      },
    },
  },
  {
    image: "assets/radioactive-materials-vr.png",
    variant: "is-lab",
    content: {
      sr: {
        title: "Upravljanje radioaktivnim materijalima",
        label: "Laboratorijski scenario",
        alt: "VR obuka za upravljanje radioaktivnim materijalima",
        description:
          "VR obuka pomaze timovima da uvezbaju protokole, zastitnu opremu, kontrolu izlozenosti i reakcije na proceduralne greske.",
        points: [
          "Rad sa zastitnom opremom i barijerama",
          "Kontrola pristupa i merenje izlozenosti",
          "Proceduralne provere pre i posle rada",
        ],
      },
      en: {
        title: "Radioactive material handling",
        label: "Laboratory scenario",
        alt: "VR training for radioactive material handling",
        description:
          "VR training helps teams practice protocols, protective equipment, exposure control, and responses to procedural errors.",
        points: [
          "Use of protective equipment and barriers",
          "Access control and exposure monitoring",
          "Procedural checks before and after work",
        ],
      },
    },
  },
];

const supportedLanguages = ["sr", "en"];
const scenarioGrid = document.querySelector("[data-scenario-grid]");
const header = document.querySelector("[data-header]");
const navToggle = document.querySelector("[data-nav-toggle]");
const languageButtons = document.querySelectorAll("[data-language-option]");
const metaDescription = document.querySelector('meta[name="description"]');

function getNestedValue(source, path) {
  return path.split(".").reduce((value, key) => value?.[key], source);
}

function getInitialLanguage() {
  const savedLanguage = localStorage.getItem("siteLanguage");
  if (supportedLanguages.includes(savedLanguage)) {
    return savedLanguage;
  }

  const browserLanguage = navigator.language?.slice(0, 2);
  return supportedLanguages.includes(browserLanguage) ? browserLanguage : "sr";
}

function renderScenarios(language) {
  scenarioGrid.innerHTML = scenarios
    .map((scenario) => {
      const content = scenario.content[language];

      return `
        <article class="scenario-card ${scenario.variant}">
          <img class="scenario-image" src="${scenario.image}" alt="${content.alt}" loading="lazy" />
          <div class="scenario-body">
            <span class="scenario-kicker">${content.label}</span>
            <h3>${content.title}</h3>
            <p>${content.description}</p>
            <ul class="scenario-list">
              ${content.points.map((point) => `<li>${point}</li>`).join("")}
            </ul>
          </div>
        </article>
      `;
    })
    .join("");
}

function applyTranslations(language) {
  const dictionary = translations[language];

  document.documentElement.lang = language;
  document.title = dictionary.metaTitle;
  metaDescription.setAttribute("content", dictionary.metaDescription);

  document.querySelectorAll("[data-i18n]").forEach((element) => {
    const translatedText = getNestedValue(dictionary, element.dataset.i18n);
    if (translatedText) {
      element.textContent = translatedText;
    }
  });

  document.querySelectorAll("[data-i18n-aria-label]").forEach((element) => {
    const translatedLabel = getNestedValue(dictionary, element.dataset.i18nAriaLabel);
    if (translatedLabel) {
      element.setAttribute("aria-label", translatedLabel);
    }
  });

  languageButtons.forEach((button) => {
    const isActive = button.dataset.languageOption === language;
    button.setAttribute("aria-pressed", String(isActive));
  });

  localStorage.setItem("siteLanguage", language);
  renderScenarios(language);
}

function updateHeaderState() {
  header.classList.toggle("is-scrolled", window.scrollY > 12);
}

applyTranslations(getInitialLanguage());
updateHeaderState();

window.addEventListener("scroll", updateHeaderState, { passive: true });

navToggle.addEventListener("click", () => {
  header.classList.toggle("is-open");
});

languageButtons.forEach((button) => {
  button.addEventListener("click", () => {
    applyTranslations(button.dataset.languageOption);
    header.classList.remove("is-open");
  });
});

document.querySelectorAll(".main-nav a").forEach((link) => {
  link.addEventListener("click", () => header.classList.remove("is-open"));
});
